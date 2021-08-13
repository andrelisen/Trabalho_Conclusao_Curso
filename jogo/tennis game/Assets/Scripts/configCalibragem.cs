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
    public GameObject txtInstrucao;
    //GameObject que contém o txt para indicar posição
    public GameObject txtMover;
    //GameObject com o ícone indicativo para onde mover o sensor
    public GameObject iconeIndicativoEsquerda;
    //GameObject com o ícone indicativo para onde mover o sensor
    public GameObject iconeIndicativoDireita;
    //Variável para controlar para onde será feita a calibragem
    public static int posicaoCalibragem;
    //GameObject com o btn das inicializacoes das calibragens
    public GameObject botaoInit;
    //GameObject com controle das saidas de quando for concluida as calibragens
    public GameObject txtSaidaDireita;
    public GameObject txtSaidaEsquerda;
    //GameObject com o btn ao final da calibragem para ir para a configuração da partida
    public GameObject botaoSeguir;
    void Start()
    {
        string [] portasDisponiveis = SerialPort.GetPortNames(); //coleta porta serial disponivel
        //Debug.Log(portasDisponiveis[0]); //seleciona a primeira que é a que está conectado c/ comun serial c/ arduino
        //Cria porta e abre 
        //porta = new SerialPort("/dev/ttyACM1", 115200);
        porta = new SerialPort(portasDisponiveis[0], 115200);
        porta.Open();
        porta.ReadTimeout = -1; //InfiniteTimeout = -1
        posicaoCalibragem = -1; //inicializa flag para detectar extremo de calibragem
    }

    void Update()
    {
        if(porta.IsOpen){
            try{
                //Verifica se está sendo recebido dados 
                //Debug.Log(porta.BytesToRead);
                if(porta.BytesToRead == 0){ //Não está sendo recebido dados no buffer
                    //Debug.Log(porta.BytesToRead);
                     Debug.Log("Sem recebimento de dados A!");
                }else{ //Iniciou o recebimento de dados no buffer
                    //Debug.Log("Entrada recebida!");
                    int lePorta = porta.ReadByte();
                    porta.DiscardInBuffer();
                    //Debug.Log(lePorta);
                    if(lePorta == 54){ //calibragem sera iniciada 
                        if(posicaoCalibragem == 0){ //calibragem para a direita
                            Debug.Log("Calibragem para a direita INICIADA!");
                            botaoInit.SetActive(false);
                            txtMover.SetActive(true);
                            iconeIndicativoDireita.SetActive(true);
                            iconeIndicativoEsquerda.SetActive(false);
                        }else if(posicaoCalibragem == 1){ //calibragem para a esquerda
                            Debug.Log("Calibragem para a esquerda INICIADA!");
                            botaoInit.SetActive(false);
                            txtMover.SetActive(true);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(true);
                        }
                        
                    }else if(lePorta == 55){ //calibragem concluida 
                        if(posicaoCalibragem == 0){
                            //renderizar elementos p/ iniciar a calibragem para a esquerda
                            Debug.Log("Calibragem para a direita CONCLUIDA!");
                            txtSaidaDireita.GetComponent<Text>().text = "CALIBRAGEM PARA A DIREITA  ✓ ";
                            txtSaidaEsquerda.GetComponent<Text>().text = "CALIBRAGEM PARA A ESQUERDA  ✘ ";
                            txtInstrucao.GetComponent<Text>().text = "CALIBRAGEM PARA A ESQUERDA";
                            txtMover.GetComponent<Text>().text = "MOVA RAPIDAMENTE O SENSOR PARA A ESQUERDA";
                            botaoInit.SetActive(true);
                            txtMover.SetActive(false);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(false);
                        }else{
                            txtSaidaEsquerda.GetComponent<Text>().text = "CALIBRAGEM PARA A ESQUERDA  ✓ ";
                            Debug.Log("Calibragem para a esquerda CONCLUIDA!");
                            txtInstrucao.GetComponent<Text>().text = "CALIBRAGEM CONCLUÍDA";
                            botaoSeguir.SetActive(true);
                            botaoInit.SetActive(false);
                            txtMover.SetActive(false);
                            iconeIndicativoDireita.SetActive(false);
                            iconeIndicativoEsquerda.SetActive(false);
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

    public void TesteMovimento(){
        if(porta.IsOpen){
            try{
                porta.Write("2");
            }catch(System.Exception){
                throw;
            }
        }
    }

}
