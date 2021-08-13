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
    //valor recebido do smartphone    
    int valorRecebidoSmartphone = Serial2.read();
    
    if(valorRecebidoSmartphone == '3'){ //conexão estabelecida
      digitalWrite(53, HIGH);
      delay(100);
      digitalWrite(53, LOW);
      Serial2.write("4");
    }else if(valorRecebidoSmartphone == '6'){ //aviso de inicio da calibragem 
      Serial.write("6");
    }else if(valorRecebidoSmartphone == '7'){ //calibragem concluida
      Serial.write("7");
    }else if(valorRecebidoSmartphone == 68){ //direita
      Serial.write("D");
    }else if(valorRecebidoSmartphone == 69){ //esquerda
      Serial.write("E");
    }else if(valorRecebidoSmartphone == 80){ //parado
      Serial.write("P");
    }
  }
  
  if(Serial.available()){ 
    //valor recebido da unity    
    int valorRecebidoUnity = Serial.read();
 
    if(valorRecebidoUnity == '0'){ //calibragem direita
      Serial2.write("0");
    }else if(valorRecebidoUnity == '1'){ //calibragem esquerda
      Serial2.write("1");
    }else if(valorRecebidoUnity == '2'){ //movimento
      Serial2.write("2");
    }else if(valorRecebidoUnity == '5'){ //restart
      Serial2.write("5");
    }
    
  }
       
}
