using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coletaRendDadosPartida : MonoBehaviour
{
    //GameObject placares
    public GameObject placarPlayer;
    public GameObject placarBot;
    //GameObject referente ao ganhador colocando um trofeu em cima do placar vencedor
    public GameObject indicadorPlayer;
    public GameObject indicadorBot;
    //Gameobject referente ao número total de rebates
    public GameObject numTotalRebates;
    //GameObject referente ao número de acertos e erros e suas porcentagens
    public GameObject numAcertos;
    public GameObject numErros;
    //GameObject que demonstra qual foi o modo da partida
    public GameObject modoPartida;
    //GameObject referente ao número de partidas executadas no dia da sessão deste paciente
    public GameObject aproveitamento;
    //GameObject referente a duração da partida atual
    public GameObject duracaoPartida;

    int renderizar = 0;

    

    void Update()
    {
        if(renderizar == 0){

            string escrita;

            placarPlayer.GetComponent<Text>().text = (Ball.playerScore).ToString();
            placarBot.GetComponent<Text>().text = (Ball.botScore).ToString();

            if(Ball.botScore > Ball.playerScore){
                indicadorBot.SetActive(true);
            }else if(Ball.playerScore > Ball.botScore){
                indicadorPlayer.SetActive(true);
            }


            numTotalRebates.GetComponent<Text>().text = "Total de rebates: " + (Ball.numRebates).ToString();

            numAcertos.GetComponent<Text>().text = "Número de acertos: " + (Player.numAcertos).ToString();
            numErros.GetComponent<Text>().text = "Número de erros: " +  (Ball.numErros).ToString();

            int a = Player.numAcertos;
            int r = Ball.numRebates;
            Debug.Log("Número de acertos: " + a);
            Debug.Log("Número de rebates: " + r);
            double aproveitamentoAcertos =(double) a / r;
            aproveitamentoAcertos = aproveitamentoAcertos * 100;

            Debug.Log("Aproveitamento: " + aproveitamentoAcertos);
            aproveitamento.GetComponent<Text>().text = "Aproveitamento: " + aproveitamentoAcertos.ToString("F") + "%";
            
            
            escrita = "Placar da partida: PACIENTE = " + (Ball.playerScore).ToString() + "| BOT = " + (Ball.botScore).ToString() + " Total de rebates: " + (Ball.numRebates).ToString() + " Número de acertos: " + (Player.numAcertos).ToString() + " Número de erros: " +  (Ball.numErros).ToString() + " Aproveitamento: " + aproveitamentoAcertos.ToString("F") + "%";
            
            switch(controllGame.modalidadeJogo){
                case 1: //tempo
                    modoPartida.GetComponent<Text>().text = "Modo da partida: tempo";
                    escrita = escrita + " Modo da partida: tempo";
                    break;
                case 2: //fácil 
                    modoPartida.GetComponent<Text>().text = "Modo da partida: nível fácil";
                    escrita = escrita + " Modo da partida: nível fácil";
                    break;
                case 3: //médio
                    modoPartida.GetComponent<Text>().text = "Modo da partida: nível médio";
                    escrita = escrita + " Modo da partida: nível médio";
                    break;
                case 4: //difícil
                    modoPartida.GetComponent<Text>().text = "Modo da partida: nível difícil";
                    escrita = escrita + " Modo da partida: nível difícil";
                    break;
            }

            float tempoDurantePartida = controllGame.tempoTotalPartida;
            if(tempoDurantePartida > 60f){
                float min = tempoDurantePartida/60;
                Debug.Log("Tempo decorrido é de " + min + "min");
                duracaoPartida.GetComponent<Text>().text = "Duração da partida: " + (min).ToString() + " min";
                escrita = escrita + " Duração da partida: " + (min).ToString() + " min";
            }else if(tempoDurantePartida == 60f){
                Debug.Log("Tempo decorrido é de 1 minuto");
                duracaoPartida.GetComponent<Text>().text = "Duração da partida: 1 min";
                escrita = escrita + " Duração da partida: 1 min";
            }else if(tempoDurantePartida < 60f){
                Debug.Log("Tempo decorrido é de " + tempoDurantePartida + "seg");
                duracaoPartida.GetComponent<Text>().text = "Duração da partida: " + (tempoDurantePartida).ToString() + "seg";
                escrita = escrita + " Duração da partida: " + (tempoDurantePartida).ToString() + " seg";
            }
            
            dadosJogo.Salvar(escrita);
            renderizar = 1;
            
        }
    }

}
