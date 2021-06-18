using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //alvo
    public Transform aimTarget;

    float speed = 3f; //para o movimento suave do jogador

    float force = 13;

    bool hitting;

    //referencia publica para a bola
    public Transform ball;

    //referencia ao animator
    Animator animator;

    Vector3 aimTargetInitialPosition;

    ShotManager shotManager;
    Shot currentShot;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //pegar animator do componente
        aimTargetInitialPosition = aimTarget.position;
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }

    // Update is called once per frame
    void Update()
    {
        //capturar posição se pra frente, pra trás, pra direita ou pra esquerda
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.F)){
            hitting = true; //modificar posicao do alvo
            currentShot = shotManager.topSpin;
        }else if(Input.GetKeyUp(KeyCode.F)){
            hitting = false;
        }

        if(Input.GetKeyDown(KeyCode.E)){
            hitting = true; //modificar posicao do alvo
            currentShot = shotManager.flat;
        }else if(Input.GetKeyUp(KeyCode.E)){
            hitting = false;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            hitting = true; //modificar posicao do alvo
            currentShot = shotManager.flatServe;
        }else if(Input.GetKeyUp(KeyCode.R)){
            hitting = false;
        }

        if(hitting){
            aimTarget.Translate(new Vector3(h, 0, 0) * speed * 2 * Time.deltaTime);
        }

        if((h != 0 || v != 0) && !hitting){ //em y é para ir para cima e para baixo
            //movimenta avatar
            //time.deltatime evita alta taxa de atualização de quadros
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //se não colocar uma velocidade o movimento do player é muito rapidamente
        }
    }

    private void OnTriggerEnter(Collider other){
        //obj que acabamos de colider tem uma bola?
        if(other.CompareTag("Ball")){
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

            //pegar direcao da bola para melhorar a animação - detecção em x
            Vector2 ballDir = ball.position - transform.position; //direção da bola em relação ao avatar
            if(ballDir.x >= 0){
                animator.Play("movimentoRaquete"); //mov raquete p/ dir
            }else{ 
                animator.Play("backhand"); //mov raquete p/ esq
            }
            
            aimTarget.position = aimTargetInitialPosition;

        }
    }

}
