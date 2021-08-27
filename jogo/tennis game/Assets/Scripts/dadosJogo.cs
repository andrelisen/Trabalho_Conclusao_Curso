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
    
    //Lista que guarda o nome dos fisioterapeutas
    public static List<string> fisioterapeutas = new List<string>();
    //Lista que guarda o nome dos pacientes
    public static List<string> pacientes = new List<string>();

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

    public static void Leitura(){
        Debug.Log("Lendo arquivo...");
        string nomeArquivo = Application.dataPath + "/LogJogo.txt";

        if (File.Exists(nomeArquivo)){
            try{
                using (StreamReader sr = new StreamReader(nomeArquivo)){
                    string linha;
                        // Lê linha por linha até o final do arquivo
                    while ((linha = sr.ReadLine()) != null)
                    {
                    //    Debug.Log(linha);
                       if(linha == "$"){//representa 
                            // linha = sr.ReadLine();
                            Debug.Log("Data de execução: " + sr.ReadLine());
                            string nomeFisio = sr.ReadLine();
                            //Verifica se já existe o fisioterapeuta na lista, se sim, não adiciona
                            if (fisioterapeutas.IndexOf(nomeFisio) >= 0){
                                //Element found in list.
                                Debug.Log("Exists!");
                            }else{
                                fisioterapeutas.Add(nomeFisio);
                                Debug.Log("Not Exists!");
                            }
                            Debug.Log("Fisioterapeuta: " + nomeFisio);
                            string nomePaciente = sr.ReadLine();
                            //Verifica se já existe o paciente na lista, se sim, não adiciona
                            if (pacientes.IndexOf(nomePaciente) >= 0){
                                //Element found in list.
                                Debug.Log("Exists!");
                            }else{
                                pacientes.Add(nomePaciente);
                                Debug.Log("Not Exists!");
                            }
                            Debug.Log("Paciente: " + nomePaciente);
                            Debug.Log("Idade do Paciente: " + sr.ReadLine());
                            Debug.Log("Nível da amputação: " + sr.ReadLine());
                            Debug.Log("Sexo do paciente: " + sr.ReadLine());
                       }
                       else if(linha == "#"){
                           linha = sr.ReadLine();
                           if(linha == "A"){
                                Debug.Log("Dados físicos do paciente antes da partida: " );
                                Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                           }
                        
                        //     Debug.Log("-------------DF--------------");
                       }else if(linha == "##"){
                            linha = sr.ReadLine();
                            if(linha == "P"){
                                Debug.Log("Dados da partida: " );
                                string[] separa = sr.ReadLine().Split(',');
                                Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                Debug.Log("Total de rebates: " + sr.ReadLine());
                                Debug.Log("Número de acertos: " + sr.ReadLine());
                                Debug.Log("Número de erros: " + sr.ReadLine());
                                Debug.Log("Aproveitamento: " + sr.ReadLine());
                                Debug.Log("Modo da partida: " + sr.ReadLine());
                                Debug.Log("Duração da partida: " + sr.ReadLine());
                            }
                       }else if(linha == "*"){
                            linha = sr.ReadLine();
                            if(linha == "P"){
                                Debug.Log("Dados da partida: " );
                                string[] separa = sr.ReadLine().Split(',');
                                Debug.Log("Placar: Player " + separa[0] + " Bot: " + separa[1]);
                                Debug.Log("Total de rebates: " + sr.ReadLine());
                                Debug.Log("Número de acertos: " + sr.ReadLine());
                                Debug.Log("Número de erros: " + sr.ReadLine());
                                Debug.Log("Aproveitamento: " + sr.ReadLine());
                                Debug.Log("Modo da partida: " + sr.ReadLine());
                                Debug.Log("Duração da partida: " + sr.ReadLine());
                            }else if(linha == "###"){
                                linha = sr.ReadLine();
                                if(linha == "D"){
                                    Debug.Log("Dados físicos do paciente depois da partida: " );
                                    Debug.Log("Pressão Arterial: " + sr.ReadLine());
                                    Debug.Log("Batimentos Cardiacos: " + sr.ReadLine());
                                    Debug.Log("Oxigenação do sangue: " + sr.ReadLine());
                                } 
                            }
                       }else if(linha == "####"){
                           Debug.Log("Fim do log");
                       }
                    }
                    sr.Close();
                }
            }catch{
                Debug.Log("ERRO");
            }
        }else{
            Debug.Log(" O arquivo " + nomeArquivo + "não foi localizado !");
        }
    }

}


