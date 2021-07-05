import 'package:flutter/material.dart';
import 'package:flutter_bluetooth_serial/flutter_bluetooth_serial.dart';
import 'package:coletadados_tennis/connection.dart';
import 'package:coletadados_tennis/captura.dart';

class PaginaLista extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Selecione o módulo HC-05'),
        centerTitle: true,
      ),
      body: FutureBuilder(
        future: FlutterBluetoothSerial.instance.requestEnable(),
        builder: (context, future) {
          if (future.connectionState == ConnectionState.waiting) {
            print("Esperando conexão...");
            return Scaffold(
              body: Container(
                height: double.infinity,
                child: Center(
                  child: Icon(
                    Icons.bluetooth_disabled,
                    size: 200.0,
                    color: Color(0xFF4D7DB4),
                  ),
                ),
              ),
            );
          } else if (future.connectionState == ConnectionState.done) {
            print("Conexão feita!");
            return SelecionaDispositivo(); //coleta dados do dispositivo e conecta
          } else {
            return SelecionaDispositivo();
          }
        },
      ),
    );
  }
}

//widget sem estado
class SelecionaDispositivo extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return SelectBondedDevicePage(
      onCahtPage: (device1) {
        BluetoothDevice device = device1;
        Navigator.push(
          context,
          MaterialPageRoute(
            builder: (context) {
              return AceleroPage(server: device);
            },
          ),
        );
      },
    );
  }
}
