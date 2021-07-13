using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class avatarMove : MonoBehaviour
{
    //declração da porta serial de comunicação com o arduino
    SerialPort porta;
    private int moverDir = 0;
    private int moverEsq = 0;
    void Start()
    {
        porta = new SerialPort("/dev/ttyACM0", 115200);
        porta.Open();
        porta.ReadTimeout = -1; //InfiniteTimeout = -1
        porta.DiscardInBuffer();
        // porta.ReadTimeout = 10;
    }

    void Update()
    {   
        if(porta.IsOpen){
            try{
                //Debug.Log(porta.ReadByte()); //esse funciona
                //Debug.Log(porta.ReadChar()); //esse funciona também mas vem como byte
                //Debug.Log(porta.ReadLine()); //esse funciona e captura toda a linha corretamente \o/
                //leitura da entrada
                string leitura = porta.ReadLine();
                //calcula e verifica quantidade de caracteres
                int tamanhoLeitura = leitura.Length;
                //captura tempo atual
                DateTime now = DateTime.Now;
                //utiliza o tempo em segundos p/ calcular v=a.t
                int tempo = now.Second;
                
                if(tamanhoLeitura == 4){
                    Debug.Log("Vazio");
                    moverDir = 0;
                    moverEsq = 0;
                }else if(tamanhoLeitura == 6 ){
                    // Debug.Log("Positivo");
                    // Debug.Log(leitura);
                    //separa aceleração
                    string[] leituraSep = leitura.Split(';');
                    // Debug.Log(leituraSep[0]);
                    string aceleracaoSep = leituraSep[0];
                    float aceleracao = float.Parse(aceleracaoSep);
                    float velocidade = aceleracao * tempo;
                    Debug.Log(velocidade);
                    // moveAvatar(1, 2.5f);
                    moveAvatar(1, velocidade);
                    moverDir++;
                    moverEsq = 0;
                }else if(tamanhoLeitura == 7 ){
                    // Debug.Log("Negativo");
                    // Debug.Log(leitura);
                    //separa aceleração
                    string[] leituraSep = leitura.Split(';');
                    // Debug.Log(leituraSep[0]);
                    string aceleracaoSep = leituraSep[0];
                    float aceleracao = float.Parse(aceleracaoSep);
                    int arredondar = -1;
                    float velocidade = arredondar * aceleracao * tempo;
                    Debug.Log(velocidade);
                    // moveAvatar(0, 2.5f);
                    moveAvatar(0, velocidade);
                    moverEsq++;
                    moverDir = 0;
                }

            }catch(System.Exception){
                throw;
            }
        }
    }

    void moveAvatar(int direcao, float velocidade){
        if(direcao == 1){
            transform.Translate(-Vector2.right * velocidade * Time.deltaTime, Space.World);
        }

        if(direcao == 0){
            transform.Translate(Vector2.right * velocidade * Time.deltaTime, Space.World);
        }
    }

}
