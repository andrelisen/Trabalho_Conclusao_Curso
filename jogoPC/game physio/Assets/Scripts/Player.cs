using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform aimTarget; //alvo para onde a bolinha será lançada para o lado do bot
    float speed = 7.5f; //velocidade da raquete que será multiplicada pela posição
    float force = 15;
    bool hitting;

    public Transform ball;
    
    Vector3 aimTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        aimTargetPosition = aimTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        //captura eixos de movimentação
        float h = Input.GetAxisRaw("Horizontal"); //no meu caso movimenta apenas na horizontal pois paciente tem pouca mobilidade
        float v = Input.GetAxisRaw("Vertical");

        //lógica de movimento do alvo do lado do bot  
        if(Input.GetKeyDown(KeyCode.F)){
            hitting = true; //movimentando alvo
        }else if(Input.GetKeyUp(KeyCode.F)){
            hitting = false; //não movimentando alvo
        }

        //verifica se houve movimentação do alvo, se sim
        if(hitting){
            aimTarget.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //modifica posição em x do alvo
        }

        //verifica se houve modificação nos eixos
        if((h != 0 || v != 0) && !hitting){ //parte do diferente de hitting para não mover a raquete junto ao alvo
            // transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //horizontal, para cima/baixo, vertical
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //horizontal, para cima/baixo, vertical
        }
    }

    //função para tratar colisões com a raquete -> bola + raquete 
    private void OnTriggerEnter(Collider other) {
        //verifica se a colisão foi com a bola
        if(other.CompareTag("Ball")){
            Vector3 dir = aimTarget.position - transform.position; //pega a posição do alvo para rebater a bolinha - posição atual da raquete
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0);

            Vector3 ballDir = ball.position - transform.position;
            aimTarget.position = aimTargetPosition;
        }
    }

}
