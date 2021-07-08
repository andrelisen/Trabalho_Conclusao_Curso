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
                //leitura da entrada
                string leitura = porta.ReadLine();
                //calcula e verifica quantidade de caracteres
                int tamanhoLeitura = leitura.Length;
                
                if(tamanhoLeitura == 4){
                    Debug.Log("Vazio");
                    moverDir = 0;
                    moverEsq = 0;
                }else if(tamanhoLeitura == 6 ){
                    // Debug.Log("Positivo");
                    // Debug.Log(leitura);

                    moveAvatar(1, 2.5f);
                    moverDir++;
                    moverEsq = 0;
                }else if(tamanhoLeitura == 7 ){
                    // Debug.Log("Negativo");
                    // Debug.Log(leitura);
                    moveAvatar(0, 2.5f);
                    moverEsq++;
                    moverDir = 0;
                }

            }catch(System.Exception){
                throw;
            }
        }
        // if(porta.IsOpen){
        //     try{
        //         //Debug.Log(porta.ReadByte()); //esse funciona
        //         //Debug.Log(porta.ReadChar()); //esse funciona também mas vem como byte
        //         //Debug.Log(porta.ReadLine()); //esse funciona e captura toda a linha corretamente \o/
                
        //         //lendo os valores recebidos via comun serial
        //         string leitura = porta.ReadLine();
        //         if(leitura == "2"){
        //             Debug.Log("Sem dados do smartphone no momento");
        //         }else{
        //             Debug.Log(leitura);
        //             //realiza a separação dos valores
        //             // string[] separacao = leitura.Split(';');
        //             //recebe a direção do avatar e a velocidade
        //             // string direcaoStr = separacao[0];
        //             // string velocidadeStr = separacao[2];
        //             // Debug.Log(direcaoStr);
        //             // Debug.Log(velocidadeStr);
        //             //converte a string da direcao em inteiro e a velocidade em double 
        //             // int direcao = int.Parse(direcaoStr);
        //             // float velocidade = float.Parse(velocidadeStr);
        //             // moveAvatar(direcao, velocidade);
        //         }
                
        //     }catch(System.Exception){
        //        Debug.Log("Erro");
        //     }
        // }
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
