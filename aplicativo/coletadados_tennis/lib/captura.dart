import 'dart:async';
import 'dart:convert';
import 'dart:math';
import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:flutter_bluetooth_serial/flutter_bluetooth_serial.dart';
// import 'package:sensors/sensors.dart';
import 'package:sensors_plus/sensors_plus.dart';

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
  String _dadosMovimento = "";
  //Fim da declaração

  double _calibragemParado;
  int _numCalib = 0;
  double _velocidade;
  String _aceleracao;
  String _giro;

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
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              child: ElevatedButton(
                onPressed: () async {
                  // _tempoInicial = DateTime.now().second;
                  leituraSensores();
                  // leituraSensores();
                },
                // child: Text("Ligar"),
                child: Text("Realizar captura"),
              ),
            ),
            Container(
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              // child: Text(_dadosMovimento == "" &&
              //         _aceleracaoY == "" &&
              //         _dadosMovimento == ""
              //     ? "Aguardando coleta de dados do acelerômetro"
              //     : "[x, y, z] = [$_dadosMovimento, $_aceleracaoY, $_aceleracaoZ]"),
              child: Text(_aceleracao == ""
                  ? "AGUARDANDO CAPTURA DA ACELERAÇÃO"
                  : "Aceleração = [$_aceleracao]"),
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
    connection.output.add(utf8.encode(saida + "\r\n"));
  }

  //Captura dados do sensor acelerömetro desprezando a gravidade
  //Ou seja, apenas a aceleração do usuário sobre o smartphone
  void leituraSensores() async {
    double somAcel = 0.0;
    var leituraAceleracao;
    double velocidadeAtual;
    int tempoAtual;
    //Leitura dos sensores
    //aceleração com os efeitos da gravidade - m/s²

    //aceleração com gravidade - m/s²
    accelerometerEvents.listen((AccelerometerEvent event) {
      // velocidadeAtual = sqrt(event.x * event.x + event.y * event.y);
      // tempoAtual = DateTime.now().second;

      // double distancia = velocidadeAtual * tempoAtual;

      // var distanciaShift = distancia.toStringAsFixed(2);

      setState(() {
        _aceleracao =
            (event.x).toStringAsFixed(2) + ";"; //6 positivo - 7 negativo
        // leituraAceleracao.cancel();
        print(event.x);
        _sendMessage(_aceleracao);
      });
    });

    // double girando;

    // gyroscopeEvents.listen((GyroscopeEvent event) {
    //   setState(() {
    //     girando = sqrt(event.x * event.x + event.y * event.y);
    //     print(event);
    //     _giro = (girando).toStringAsFixed(2) + ";"; //6 positivo - 7 negativo
    //     //enviar orientação,velocidade
    //     // leituraAceleracao.cancel();
    //     _sendMessage(_giro);
    //   });
    // });
  }

  //Função responsável pela calibragem do sensor
  void calibragemSensores(int condicao) async {
    double somAcel = 0.0;

    var aceleracaoAtual;
    var leituraAceleracao;
    var velocidade;

    print("Calibragem sensores");
    if (condicao == 1) {
      //aceleração sem a gravidade
      leituraAceleracao =
          userAccelerometerEvents.listen((UserAccelerometerEvent event) {
        somAcel += event.x;
        _velocidade =
            sqrt(event.x * event.x + event.y * event.y + event.z * event.z);
        _numCalib++;
        if (_numCalib == 60) {
          leituraAceleracao.cancel();
          _calibragemParado = somAcel / _numCalib;
          aceleracaoAtual = _calibragemParado.toStringAsFixed(2);
          velocidade = _velocidade.toStringAsFixed(2);
          print("Calibragem parado é = $aceleracaoAtual");
          print("Velocidade parado é = $velocidade");
        }
      });
    }
  }
}
