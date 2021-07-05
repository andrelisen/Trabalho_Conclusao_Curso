import 'dart:async';
import 'dart:math';
import 'dart:convert';
import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:flutter_bluetooth_serial/flutter_bluetooth_serial.dart';
import 'package:sensors/sensors.dart';

class AceleroPage extends StatefulWidget {
  final BluetoothDevice server;

  const AceleroPage(
      {this.server}); //recebo o end do dispositivo selecionado para conexão bluetooth

  @override
  _AceleroPage createState() => new _AceleroPage();
}

class _AceleroPage extends State<AceleroPage> {
  BluetoothConnection connection;

  bool isConnecting = true;
  bool get isConnected => connection != null && connection.isConnected;

  bool isDisconnecting = false;

  //Variáveis de retorno (mensagem) e captura da aceleração nos três eixos - x, y, z
  String _mensagem = "";
  String _aceleracaoX = "";
  //Fim da declaração

  //Variáveis para utilizar nos calculos de distância e velocidade
  int _tempoInicial; //time between capture of datas
  double _distancia = 0; //distancia deslocada
  // Armazena o ultimo valor de aceleração e velocidade dentro da própria
  // função declarando a variável como static (no 1º momento ela será 0)
  double _aceleracaoAnterior = 0.0;
  double _velocidadeAnterior = 0.0;

  int _contador = 0;
  double _valor = 0.0;

  @override
  void initState() {
    super.initState();

    BluetoothConnection.toAddress(widget.server.address).then((_connection) {
      print('Conectado ao dispositivo');
      connection = _connection;
      setState(() {
        isConnecting = false;
        isDisconnecting = false;
      });

      connection.input.listen(_onDataReceived).onDone(() {
        // Exemplo: Detectar qual lado fechou a conexão
        // Deve haver um sinalizador `isDisconnecting` para mostrar se estamos (localmente)
        // no meio do processo de desconexão, deve ser definido antes de chamar
        // `dispose`,` finish` ou `close`, que causa a desconexão.
        // Se excluirmos a desconexão, `onDone` deve ser disparado como resultado.
        // Se não excluíssemos isso (sem sinalizador definido), significa fechar por remoto.
        if (isDisconnecting) {
          print('Desconectando localmente!');
        } else {
          print('Desconectado remotamente!');
        }
        if (this.mounted) {
          setState(() {});
        }
      });
    }).catchError((error) {
      print('Não é possível conectar, ocorreu uma exceção');
      print(error);
    });
  }

  @override
  void dispose() {
    // Avoid memory leak (`setState` after dispose) and disconnect
    if (isConnected) {
      isDisconnecting = true;
      connection.dispose();
      connection = null;
    }

    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          title: (isConnecting
              ? Text('Conectando a ' + widget.server.name + '...')
              : isConnected
                  ? Text('Conectado com ' + widget.server.name)
                  : Text('Log com ' + widget.server.name))),
      body: SafeArea(
        child: Column(
          children: [
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 100, 0, 0),
              //child: Text("Ligar/desligar LED"),
              child: Text("CAPTURA DE DADOS DE MOVIMENTAÇÃO"),
            ),
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              child: ElevatedButton(
                onPressed: () async {
                  _aceleracaoAnterior = 0.0;
                  _velocidadeAnterior = 0.0;
                  _tempoInicial = DateTime.now().second;
                  print("Tempo capturado inicialmente: $_tempoInicial");
                  // connection.output.add(utf8.encode("H"));
                  leituraSensores();
                  // leituraSensores();
                },
                // child: Text("Ligar"),
                child: Text("CAPTURAR"),
              ),
            ),
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              child: Text(_mensagem == ""
                  ? "Aguardando retorno do Arduino"
                  : "Retorno Arduino: $_mensagem"),
            ),
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              // child: Text(_aceleracaoX == "" &&
              //         _aceleracaoY == "" &&
              //         _aceleracaoX == ""
              //     ? "Aguardando coleta de dados do acelerômetro"
              //     : "[x, y, z] = [$_aceleracaoX, $_aceleracaoY, $_aceleracaoZ]"),
              child: Text(_aceleracaoX == ""
                  ? "AGUARDANDO CAPTURA DA ACELERAÇÃO"
                  : "x = [$_aceleracaoX]"),
            ),
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              // child: Text(_aceleracaoX == "" &&
              //         _aceleracaoY == "" &&
              //         _aceleracaoX == ""
              //     ? "Aguardando coleta de dados do acelerômetro"
              //     : "[x, y, z] = [$_aceleracaoX, $_aceleracaoY, $_aceleracaoZ]"),
              child: Text(_distancia == 0.0
                  ? "AGUARDANDO CAPTURA DA DISTÂNCIA"
                  : "x = [$_distancia]"),
            ),
          ],
        ),
      ),
    );
  }

  //recebe dados via bluetooth
  void _onDataReceived(Uint8List data) {
    print("Recebendo uma mensagem do módulo!");
    String entrada = new String.fromCharCodes(data);
    print(entrada);
    setState(() {
      _mensagem = "Ok!";
    });
    print(entrada);
  }

  //envia dados via bluetooth
  void _sendMessage(String saida) async {
    // print("Enviando uma mensagem ao módulo!");
    saida = saida.trim();
    connection.output
        .add(utf8.encode(saida + "\r\n")); //emite saida para o modulo
  }

  //Captura dados do sensor acelerömetro desprezando a gravidade
  //Ou seja, apenas a aceleração do usuário sobre o smartphone
  void leituraSensores() async {
    double ax = 0.0;

    //Leitura dos sensores
    //aceleração com os efeitos da gravidade - m/s²
    accelerometerEvents.listen((AccelerometerEvent event) {
      // print(event);
      ax = event.x;
      setState(() {
        _aceleracaoX = ax.toStringAsFixed(3) + ";"; //com ; p/ enviar p/ arduino
        _sendMessage(_aceleracaoX);
      });
    });

    //aceleração sem gravidade - ação do usuário no smartphone - m/s²
    userAccelerometerEvents.listen((UserAccelerometerEvent event) {
      // print("ACELERAÇÃO SEM GRAVIDADE!");
      // ax = event.x;
    });

    //rotação do dispositivo - rad/s
    gyroscopeEvents.listen((GyroscopeEvent event) {
      // print("DADOS DO GIROSCÓPIO"); //em rad/s
    });

    double calculoTrapezio(double aceleracao, int tempoAgora) {
      double velocidade = 0.0;
      double distanciaCalculada = 0.0;

      print("Variáveis para o cálculo da Regra do Trapézio:");
      print("Velocidade anterior = $_velocidadeAnterior");
      print("Aceleração anterior = $_aceleracaoAnterior");
      print("Tempo = $tempoAgora");
      print("Aceleração = $aceleracao");

      velocidade = _velocidadeAnterior +
          (_aceleracaoAnterior + aceleracao) * tempoAgora / 2.0;

      distanciaCalculada =
          _distancia + (_velocidadeAnterior + velocidade) * tempoAgora / 2.0;

      _distancia = distanciaCalculada;
      _aceleracaoAnterior = aceleracao;
      _velocidadeAnterior = velocidade;

      print("Velocidade calculada: $velocidade");
      print("Distância calculada: $distanciaCalculada");

      return distanciaCalculada;
    }

    //timer para coletar os dados de aceleração a cada 50ms
    Timer.periodic(Duration(milliseconds: 30), (Timer t) {
      setState(() {
        _aceleracaoX = ax.toStringAsFixed(3) + ";"; //com ; p/ enviar p/ arduino
        _sendMessage(_aceleracaoX);
        //       // int tempoAgora = DateTime.now().second - _tempoInicial;
        //       // // double distancia = calculoTrapezio(aceleracao, tempoAgora);
        //       // _tempoInicial = DateTime.now().second;
      });
    });
  }
}
