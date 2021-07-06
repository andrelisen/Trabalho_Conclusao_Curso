#include <SerialCommand.h>
//Faz comunicação com o arduino enviando informação para mover para a direita ou esquerda
//Recebe a direcao
//Emite uma mensagem do sistema quando necessário enviar o log da partida
//Tudo isso usando módulo HC-05
//Posição lógica das entradas: branco, cinza [branco = TX2(RX), cinza = RX2(TX)]


void setup() {
  //Seta porta serial de comunicação
  Serial.begin(115200);
  Serial2.begin(115200); //TX2, RX2 
  Serial.flush();
  Serial2.flush();
  //led apenas para testes de recebimento/envio de dados
  pinMode(53, OUTPUT);
}

void loop() {
if(Serial2.available()){
  Serial.write(Serial2.read());
}
//  while (Serial2.available() >= 4) {
//    switch (Serial2.read()) {
//      case 's':
//        Serial2.read(); // Ignore (probably) 't'
//        switch (Serial2.read()) {
//          case 'a': // "start"
//            digitalWrite(53, HIGH);
//            
//            Serial2.read(); // Ignore 'r'
//            while (Serial2.available() == 0);
//            Serial2.read(); // Ignore 't'
//            break;
//            
//          case 'o': // "stop"
//            
//            digitalWrite(53, LOW);
//            
//            Serial2.read(); // Ignore 'p'
//        }
//        break;
//    }
//  }

//while (Serial2.available() >= 4) {
//   Serial.write(Serial2.read());
//    switch (Serial2.read()) {
//      case '0':
//        Serial2.read(); // Ignore (probably) 't'
//        switch (Serial2.read()) {
//          case '2': // "start"
//            digitalWrite(53, HIGH);
//            
//            Serial2.read(); // Ignore 'r'
//            while (Serial2.available() == 0);
//            Serial2.read(); // Ignore 't'
//            break;
//            
//          case 'o': // "stop"
//            
//            digitalWrite(53, LOW);
//            
//            Serial2.read(); // Ignore 'p'
//           
//        }
//        
//        break;
//    }
//    
//  }
  
  //comunicaçao smartphone - arduino - unity
//  if(Serial2.available()){
//    int isByte = Serial2.read();
////    Serial.write((char)isByte);
//     Serial.print(Serial2.read());
//    Serial2.flush();
//  }
//  delay(50);

  //Comunicaçao unity - arduino 
//  if(Serial.available()){
//    data = Serial.read();
//    if(data == '1'){
//      digitalWrite(53, HIGH);
//      delay(1000);
//      digitalWrite(53, LOW);
//      delay(1000);
//    }
//  }
  
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
  
    
}
