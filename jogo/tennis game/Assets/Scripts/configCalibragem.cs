using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class configCalibragem : MonoBehaviour
{
    //Cria variável para a Comunicação serial
    public static SerialPort porta;

    //GameObject que contém o texto de saída se direita ou esquerda
    public GameObject txtPosicao;
    //GameObject que contém o txt para indicar posição
    public GameObject txtMover;
    //GameObject com o ícone indicativo para onde mover o sensor
    public GameObject iconeIndicativo;
    //Variável para controlar para onde será feita a calibragem
    public static int posicaoCalibragem;
    void Start()
    {
        //Cria porta e abre 
        porta = new SerialPort("/dev/ttyACM0", 115200);
        porta.Open();
        porta.ReadTimeout = -1; //InfiniteTimeout = -1
        posicaoCalibragem = -1;
    }

    void Update()
    {
        if(porta.IsOpen){
            try{
                //Verifica se está sendo recebido dados 
                //Debug.Log(porta.BytesToRead);
                if(porta.BytesToRead == 0){ //Não está sendo recebido dados no buffer
                    //Debug.Log(porta.BytesToRead);
                     Debug.Log("Sem recebimento de dados!");
                }else{ //Iniciou o recebimento de dados no buffer
                    //Debug.Log("Entrada recebida!");
                    int lePorta = porta.ReadByte();
                    porta.DiscardInBuffer();
                    Debug.Log(lePorta);
                    //saida.GetComponent<Text>().text = "SAÍDA:" + lePorta;
                    if(lePorta == 54){ //calibragem sera iniciada em 15segundos
                        //renderiza tela de contagem
                    }else if(lePorta == 55){ //calibragem concluida 
                        if(posicaoCalibragem == 0){
                            //renderizar elementos da calibragm para a esquerda
                        }
                    }
                }
            }catch(System.Exception){
                throw;
            }
        }
    }

    public void TesteComunicacao(int opcao){
        if(porta.IsOpen){
            try{
                if(opcao == 0){
                    porta.Write("0");
                }else if(opcao == 1){
                    porta.Write("1");
                }else if(opcao == 2){
                    porta.Write("2");
                }
            }catch(System.Exception){
                throw;
            }
        }
    }

    public void Calibragem(){
        posicaoCalibragem += 1;
        if(porta.IsOpen){
            try{
                if(posicaoCalibragem == 0){
                    //direita
                    porta.Write("0");
                    Debug.Log("Calibragem direita");
                }else if(posicaoCalibragem == 1){
                    //esquerda
                    porta.Write("1");
                    Debug.Log("Calibragem esquerda");
                }
            }catch(System.Exception){
                throw;
            }
        }
    }

    public void configuraElementosTela(int condicao){
        //quando está no estado de aguardando conexão
        if(condicao == 1){

        }else if(condicao == 2){ //quando a conexão bluetooth foi estabelecida

        }else if(condicao == 3){ //

        }
    }
}
