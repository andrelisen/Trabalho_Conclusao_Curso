using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class retornarCena : MonoBehaviour
{
    public void Retornar(string nomeCena){
        if(coletaDadosFisicos.opcao == 2){
            UnityEngine.SceneManagement.SceneManager.LoadScene("sceneAcessoHistSair");
        }else{
            UnityEngine.SceneManagement.SceneManager.LoadScene(nomeCena);
        }
    }
}
