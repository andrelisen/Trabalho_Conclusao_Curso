import 'dart:async';
import 'dart:convert';
import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:flutter_bluetooth_serial/flutter_bluetooth_serial.dart';
import 'package:sensors/sensors.dart';

class ChatPage extends StatefulWidget {
  final BluetoothDevice server;

  const ChatPage(
      {this.server}); //recebo o end do dispositivo selecionado para conexão bluetooth

  @override
  _ChatPage createState() => new _ChatPage();
}

class _Message {
  int whom;
  String text;

  _Message(this.whom, this.text);
}

class _ChatPage extends State<ChatPage> {
  static final clientID = 0;
  BluetoothConnection connection;

  List<_Message> messages = [];
  String _messageBuffer = '';

  final TextEditingController textEditingController =
      new TextEditingController();
  final ScrollController listScrollController = new ScrollController();

  bool isConnecting = true;
  bool get isConnected => connection != null && connection.isConnected;

  bool isDisconnecting = false;

  //Variaveis que eu inseri :)
  String _mensagem = "";
  String _aceleracaoX = "";
  String _aceleracaoY = "";
  String _aceleracaoZ = "";
  //Fim da declaração

  String _aceleX = "";
  String _aceleY = "";
  String _aceleZ = "";

  String _giroX = "";
  String _giroY = "";
  String _giroZ = "";

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
            // Container(
            //   alignment: Alignment.center,
            //   padding: const EdgeInsets.fromLTRB(0, 100, 0, 0),
            //   //child: Text("Ligar/desligar LED"),
            //   child: Text("Movimentar Avatar"),
            // ),
            // Container(
            //   alignment: Alignment.center,
            //   padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
            //   child: ElevatedButton(
            //     onPressed: () async {
            //       // connection.output.add(utf8.encode("H"));
            //       _sendMessage("H");
            //     },
            //    // child: Text("Ligar"),
            //    child: Icon(Icons.arrow_forward_ios),
            //   ),
            // ),
            // Container(
            //   alignment: Alignment.center,
            //   padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
            //   child: ElevatedButton(
            //     onPressed: () async {
            //       // connection.output.add(utf8.encode("L"));
            //       _sendMessage("L");
            //     },
            //     //child: Text("Desligar"),
            //     child: Icon(Icons.arrow_back_ios),
            //   ),
            // ),
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
                  ? "Aguardando retorno"
                  : "Retorno: $_mensagem"),
            ),
            Container(
              //PAREI AQUI A PROGRAMAÇÃO DIA 5/ABRIL - TROCAR POR AX, AY, AZ E ENVIAR PARA MODULO
              alignment: Alignment.center,
              padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
              child: Text(_aceleracaoX == "" &&
                      _aceleracaoY == "" &&
                      _aceleracaoX == ""
                  ? "Aguardando coleta de dados do acelerômetro"
                  : "[x, y, z] = [$_aceleracaoX, $_aceleracaoY, $_aceleracaoZ]"),
            ),
            // Container(
            //   //PAREI AQUI A PROGRAMAÇÃO DIA 5/ABRIL - TROCAR POR AX, AY, AZ E ENVIAR PARA MODULO
            //   alignment: Alignment.center,
            //   padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
            //   child: Text(_aceleracaoX == "" &&
            //           _aceleracaoY == "" &&
            //           _aceleracaoX == ""
            //       ? "Aguardando coleta de dados do acelerômetro sem efeitos da gravidade"
            //       : "[x, y, z] = [$_aceleX, $_aceleY, $_aceleZ]"),
            // ),
            // Container(
            //   //PAREI AQUI A PROGRAMAÇÃO DIA 5/ABRIL - TROCAR POR AX, AY, AZ E ENVIAR PARA MODULO
            //   alignment: Alignment.center,
            //   padding: const EdgeInsets.fromLTRB(0, 50, 0, 0),
            //   child: Text(
            //       _aceleracaoX == "" && _aceleracaoY == "" && _aceleracaoX == ""
            //           ? "Aguardando coleta de dados do giroscópio"
            //           : "[x, y, z] = [$_giroX, $_giroY, $_giroZ]"),
            // ),
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
    // if (entrada == "1") {
    //   setState(() {
    //     _mensagem = "Direita";
    //   });
    // } else {
    //   setState(() {
    //     _mensagem = "Esquerda";
    //   });
    // }
  }

  //envia dados via bluetooth
  void _sendMessage(String saida) async {
    print("Enviando uma mensagem ao módulo!");
    saida = saida.trim();
    connection.output
        .add(utf8.encode(saida + "\r\n")); //emite saida para o modulo
  }

  //Captura dados do sensor acelerömetro desprezando a gravidade
  //Ou seja, apenas a aceleração do usuário sobre o smartphone
  void leituraSensores() async {
    var leituraAnterior = new DateTime.now();
    var ax = 0.0, ay = 0.0, az = 0.0;
    var a2x = 0.0, a2y = 0.0, a2z = 0.0;
    var gx = 0.0, gy = 0.0, gz = 0.0;
    //Leitura dos sensores

    //aceleração com os efeitos da gravidade - m/s²
    accelerometerEvents.listen((AccelerometerEvent event) {
      //quais eixos vou utilizar?
      // print(event);
      ax = event.x;
      ay = event.y;
      az = event.z;
    });

    //aceleração sem gravidade - ação do usuário no smartphone - m/s²
    userAccelerometerEvents.listen((UserAccelerometerEvent event) {
      // print(event);
      a2x = event.x;
      a2y = event.y;
      a2z = event.z;
      // ax = event.x;
      // ay = event.y;
      // az = event.z;
    });

    //rotação do dispositivo - rad/s
    gyroscopeEvents.listen((GyroscopeEvent event) {
      //recebe os dados em rad/s
      // print(event);
      gx = event.x;
      gy = event.y;
      gz = event.z;
    });

    Timer.periodic(Duration(seconds: 1), (Timer t) {
      // print("Executando Timer...");

      // print("Acelerômetro:");
      // print("Em x:" + ax.toStringAsFixed(3));
      // print("Em y:" + ay.toStringAsFixed(3));
      // print("Em z:" + az.toStringAsFixed(3));
      // print("------");

      // print("Giroscópio:");
      // print("Em x:" + gx.toStringAsFixed(3));
      // print("Em y:" + gy.toStringAsFixed(3));
      // print("Em z:" + gz.toStringAsFixed(3));
      // print("------");

      // setState(() {
      //   _aceleracaoX = ax.toStringAsFixed(3);
      //   _aceleracaoY = ay.toStringAsFixed(3);
      //   _aceleracaoZ = az.toStringAsFixed(3);
      //   _sendMessage(_aceleracaoX);
      // });
      setState(() {
        _aceleracaoX = ax.toStringAsFixed(3) + ";";
        _aceleracaoY = ay.toStringAsFixed(3) + ";";
        _aceleracaoZ = az.toStringAsFixed(3) + ";";
        _sendMessage(_aceleracaoX);
      });
      var dDay = DateTime.now();

      int difference = dDay.difference(leituraAnterior).inSeconds;

      if (difference > 10) {
        // print("Atualizar leitura de tempo...");
        leituraAnterior = new DateTime.now();
      }
    });
  }
}
