using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coletaDadosFisicos : MonoBehaviour
{
    //GameObjects para a coleta de dados físicos    
    public GameObject pressaoArterial;
    public GameObject batimentosCardiacos;
    public GameObject oxigenacaoSangue;
    public GameObject observacoes;

    //Flag para saber qual coleta de dados é: antes ou após partida
    public static int opcao;

    //Função para salvar os dados físicos do paciente
    public void SalvarDadosFisicos(){
        if(opcao == 1){ //Antes da partida
            string escrita;
            escrita = "ANTES: \n Pressão Arterial: " + pressaoArterial.GetComponent<Text>().text + "; Batimentos Cardiacos: " + batimentosCardiacos.GetComponent<Text>().text + "; Oxigenação do Sangue: " + oxigenacaoSangue.GetComponent<Text>().text + "; Observações: " + observacoes.GetComponent<Text>().text;
            // dadosJogo.pressaoArterialAntes = pressaoArterial.GetComponent<Text>().text;
            // dadosJogo.batimentosCardiacosAntes = batimentosCardiacos.GetComponent<Text>().text;
            // dadosJogo.oxigenacaoSangueAntes = oxigenacaoSangue.GetComponent<Text>().text;
            // dadosJogo.observacoesAntes = observacoes.GetComponent<Text>().text;
            dadosJogo.Salvar(escrita);
        }else if(opcao == 2){ //Após a partida
            // dadosJogo.pressaoArterialDepois = pressaoArterial.GetComponent<Text>().text;
            // dadosJogo.batimentosCardiacosDepois = batimentosCardiacos.GetComponent<Text>().text;
            // dadosJogo.oxigenacaoSangueDepois = oxigenacaoSangue.GetComponent<Text>().text;
            // dadosJogo.observacoesDepois = observacoes.GetComponent<Text>().text;
            // dadosJogo.CreateText();
            string escrita;
            escrita = "DEPOIS: \n Pressão Arterial: " + pressaoArterial.GetComponent<Text>().text + "; Batimentos Cardiacos: " + batimentosCardiacos.GetComponent<Text>().text + "; Oxigenação do Sangue: " + oxigenacaoSangue.GetComponent<Text>().text + "; Observações: " + observacoes.GetComponent<Text>().text;
            dadosJogo.Salvar(escrita);
        }
    }

}
