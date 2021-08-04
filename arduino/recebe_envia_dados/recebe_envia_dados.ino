#include <SerialCommand.h>
//Faz comunicação com o arduino enviando informação para mover para a direita ou esquerda
//Recebe a direcao
//Emite uma mensagem do sistema quando necessário enviar o log da partida
//Tudo isso usando módulo HC-05
//Posição lógica das entradas: branco, cinza [branco = TX2(RX), cinza = RX2(TX)]

bool flagSmartp = false;
bool flagPc = false;

void setup() {
  //Seta porta serial de comunicação
  Serial.begin(115200);
  Serial2.begin(115200); //TX2, RX2 
  Serial.flush();
  Serial2.flush();
  //led apenas para testes de recebimento/envio de dados
  pinMode(53, OUTPUT);
  pinMode(13, OUTPUT);
}

void loop() {
  if(Serial2.available()){
    if(Serial2.read() == '3'){
      Serial2.write("4");
    }
//      Serial.write(Serial2.read());
  }
  if(Serial.available()){ 
    if(Serial.read() == '1' || Serial.read() == '0'){ //Calibragem
      Serial2.write(Serial.read()); 
    }else if(Serial.read() == '2'){ //Movimento
      Serial2.write("2"); 
    }else if(Serial.read() == '5'){ //Restart
      Serial2.write("5");
    }
  }
       
}
