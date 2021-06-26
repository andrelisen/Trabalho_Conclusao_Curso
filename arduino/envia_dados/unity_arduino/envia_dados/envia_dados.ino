#include <SerialCommand.h>
//Faz comunicação com o arduino enviando informação para mover para a direita ou esquerda
//Recebe a direcao
//Emite uma mensagem do sistema quando necessário enviar o log da partida
//Tudo isso usando módulo HC-05
//Posição lógica das entradas: branco, cinza [branco = TX2(RX), cinza = RX2(TX)]

char direcao;
String leitura;
char caract;
int data;

void setup() {
  //Seta porta serial de comunicação
  Serial.begin(9600);
  Serial2.begin(9600); //TX2, RX2 
  //led apenas para testes de recebimento/envio de dados
  pinMode(53, OUTPUT);
}

void loop() {

  if(Serial2.available()){
    Serial.write(Serial2.read());
  }
  delay(20);
  
  //SITE QUE TEM UMA BOA EXPLICAÇÃO DE CNCATENAÇÃO DE BYTES E DEPOIS MUDA PARA CHAR 
  //https://www.codeproject.com/Articles/1254611/Bluetooth-Messenger

  
  //Códigos abaixo funcionam mas com bug de delay dos dados até a unity
  //Se houve recebimento de alguma informação
  //Comunicação do smartphone com o Arduino
//  while(Serial2.available()){
//      data = Serial2.read(); 
//      delay(50);
//      Serial.write(data);
//      //Serial.print(Serial2.read()); //escrever o que lê na porta serial para comunicar com a unity 
//      Serial.flush();
//      delay(20);    
//  }
  //Comunicaçao efetuada da Unity para o Arduino 
//  while(Serial.available()){
//    data = Serial.read();
//    if(data == '1'){
//      digitalWrite(53, HIGH);
//      delay(1000);
//      digitalWrite(53, LOW);
//      delay(1000);
//    }
//  }
    
}
