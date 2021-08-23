using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class dadosJogo : MonoBehaviour
{
    //Dados fisioterapeuta
    public static string nomeFisioterapeuta;
    //Dados paciente
    public static string nomePaciente;
    public static string nivelAmputacao;
    public static string idade;
    public static string sexo;
    //Dados fisicos do paciente antes da partida
    public static string pressaoArterialAntes;
    public static string batimentosCardiacosAntes;
    public static string oxigenacaoSangueAntes;
    public static string observacoesAntes;
    //Dados fisicos do paciente apos partida
    public static string pressaoArterialDepois;
    public static string batimentosCardiacosDepois;
    public static string oxigenacaoSangueDepois;
    public static string observacoesDepois;
    //Dados da partida
    public static int numeroJogadas;
    
    

    //Funçao para salvar dados em arquivo 
    public static void CreateText(){

        //Path of the file
        string path = Application.dataPath + "/Log.txt";
        //Create file if doesn't exist
        if(!File.Exists(path)){
            File.WriteAllText(path, "Logs TennisGame Physio \n\n");
        }
        //Add some to text to it
        string logger = "Log date:" + System.DateTime.Now + "\n";
        string dadosCadastrais = "Fisioterapeuta: " + nomeFisioterapeuta + " Paciente: " + nomePaciente + " Nível Amputação: " + nivelAmputacao + " Idade: " + idade + " Sexo: " + sexo + "\n";
        string dadosFisicosAntes = "Dados físicos antes da sessão: " + "Pressão Arterial: " + pressaoArterialAntes + "Batimentos cardíacos: " + batimentosCardiacosAntes + "Oxigenação do sangue: " + oxigenacaoSangueAntes + "Observações: " + observacoesAntes + "\n";
        string dadosFisicosDepois = "Dados físicos depois da sessão: " + "Pressão Arterial: " + pressaoArterialDepois + "Batimentos cardíacos: " + batimentosCardiacosDepois + "Oxigenação do sangue: " + oxigenacaoSangueDepois + "Observações: " + observacoesDepois + "\n";
        string escrever = logger + dadosCadastrais + dadosFisicosAntes + dadosFisicosDepois;
        File.AppendAllText(path, escrever);
    }

    public static void Salvar(string linha){
        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        if (!File.Exists(nomeArquivo)){
            File.Create(nomeArquivo).Close();
        }

        TextWriter arquivo = File.AppendText(nomeArquivo);
        arquivo.WriteLine(linha);
        arquivo.Close();
    }

}


