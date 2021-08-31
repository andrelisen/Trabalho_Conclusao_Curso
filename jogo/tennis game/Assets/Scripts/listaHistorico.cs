using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class listaHistorico : MonoBehaviour
{
    //referência com a configuração pronta
    public GameObject itemTemplate;
    //Local para onde deve ser adicionado o item
    public GameObject content;

    //Escrever nome do paciente na tela 
    public GameObject paciente;

    //referência para a configuração do select das sessões
    public GameObject itemSessao;
    //Lista para guardar a referencia de todas as sessões criadas via toggle
    public static List<Toggle> toggleSessoes = new List<Toggle>();

    //Mensagem de erro caso não seja selecionado nenhuma sessão
    public GameObject msgErro;
    //Lista que guarda quais sessões foram selecionadas para gerar o gráfico
    public static List<string> gerarSessoes = new List<string>();

    public void AddButt(){

        for(int i = 0; i<5; i++){
            //faz uma cópia do template
            var copy = Instantiate(itemTemplate);

            //adiciona copia dentro do content
            copy.transform.SetParent(content.transform, false);
        }
        
    }

    void Start(){

        // dadosJogo.Leitura();
        dadosJogo.LeituraDPartida(listaPacientes.nomePaciente);

        toggleSessoes = new List<Toggle>();
        gerarSessoes = new List<string>();

        // dadosJogo.LeituraDPartida("Aline");
        // for(int i = 0; i<51; i++){
        //     //faz uma cópia do template
        //     var copy = Instantiate(itemTemplate);

        //     //adiciona copia dentro do content
        //     copy.transform.SetParent(content.transform, false);
        //     copy.GetComponentInChildren<Text>().text = "SESSÃO " + i.ToString();
        //     copy.SetActive(true);
        // }

        paciente.GetComponent<Text>().text = "Paciente: " + listaPacientes.nomePaciente;

        foreach(var item in dadosJogo.dadosCadastraisPaciente){


            if(item == "Dados Cadastrais" || item == "Dados da partida") {
            // if(item.Contains("Sessão")){
                //FORMA QUANDO DADOS CADASTRAIS OU DADOS DA PARTIDA
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#2e5889", out color);
                copy.GetComponentInChildren<Text>().color = color;

                copy.SetActive(true);
                // copy.GetComponentInChildren<Toggle>().SetEnabled(false); 

                //FORMA PARA RENDERIZAR APENAS SESSÃO

                // var section = Instantiate(itemSessao);
                // section.transform.SetParent(content.transform, false);

                // section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().text = item;
                // section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                // Color color;
                // ColorUtility.TryParseHtmlString("#ffd000", out color);
                // section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().color = color;

                // toggleSessoes.Add(section.GetComponentInChildren<Toggle>());

                // section.SetActive(true);


            }else if(item.Contains("Sessão")){ //É DIFERENTE
                
                var section = Instantiate(itemSessao);
                section.transform.SetParent(content.transform, false);

                section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().text = item;
                section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                Color color;
                ColorUtility.TryParseHtmlString("#ffd000", out color);
                section.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().color = color;

                toggleSessoes.Add(section.GetComponentInChildren<Toggle>());
                section.SetActive(true);

                
            }else if(item.Contains("Aproveitamento")){
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#588157", out color);
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);
                
                // copy.GetComponentInChildren<Toggle>().SetEnabled(false);
            }else if(item.Contains("erros")){
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#bc4749", out color); //
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);

                // copy.GetComponentInChildren<Toggle>().SetEnabled(false);
            }else if(item.Contains("acertos")){
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#538d22", out color); //
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);

            }else if(item.Contains("Modo")){
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#33415c", out color); //
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);

            }else if(item.Contains("Duração")){
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#d81159", out color); //
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);
                
            }else if(item.Contains("Placar")){ 
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#006c67", out color);
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);

            }else if(item.Contains("Físicos")){ 
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#4c956c", out color);
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);
           
            }else if(item.Contains("Antes") || item.Contains("Depois") ){ 
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);

                copy.GetComponentInChildren<Text>().text = item;
                // copy.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

                //ColorUtility.TryParseHtmlString("#09FF0064", out color);

                Color color;

                ColorUtility.TryParseHtmlString("#4e598c", out color);
                copy.GetComponentInChildren<Text>().color = color;
                copy.SetActive(true);

            }else{
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);
                copy.GetComponentInChildren<Text>().text = item;
                copy.SetActive(true);
            }
        }

        // DontDestroyOnLoad(itemTemplate);
        // DontDestroyOnLoad(itemSessao);
        // DontDestroyOnLoad(content);

    }

    public void GerarGrafico(){

        int qntSelect = 0;

        foreach(var item in toggleSessoes){
            Debug.Log("Elemento adicionado é: " + item.GetComponentInChildren<Text>().text);
            Debug.Log("Ele está selecionado? " + item.isOn);
            if(item.isOn){
                qntSelect++;
                gerarSessoes.Add(item.GetComponentInChildren<Text>().text);
            }
        }

        if(qntSelect == 0){
            msgErro.SetActive(true);
        }else{
            // Destroy(itemSessao.gameObject);
            // Destroy(itemTemplate.gameObject);
            // Destroy(content.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneGraficos");
        }

    }
    // public static T instance = null;
    // void Awake() {
    //      if (instance == null){
    //          instance = this;
    //      }else{
    //          Destroy (transform.gameObject);
    //      }
    //      DontDestroyOnLoad (transform.gameObject);
    // }

}
