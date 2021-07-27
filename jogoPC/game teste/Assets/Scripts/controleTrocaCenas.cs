using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleTrocaCenas : MonoBehaviour
{

    //Função que realiza a troca de cenas enviando o nome da cena via input do botão
    public void TrocaCena(string nomeCena){
        UnityEngine.SceneManagement.SceneManager.LoadScene(nomeCena);
    }

    public void VerificaEncaminhamento(int encaminhar){
        if(encaminhar == 1){
            coletaDadosFisicos.opcao = 2;
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneColetaDadosFisicos"); //Coletar dados físicos 
        }else if(encaminhar == 2){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneConfigPartida"); //Configura nova partida
        }

        if(encaminhar == 0 && coletaDadosFisicos.opcao == 1){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneConfigPartida");
        }

        if(coletaDadosFisicos.opcao == 2 && encaminhar == 1){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneFinalizado");
        }

    }
}
