using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class Player : MonoBehaviour
{
    public Transform aimTarget; //alvo para onde a bolinha será lançada para o lado do bot
    float speed = 7.5f; //velocidade da raquete que será multiplicada pela posição
    float force = 15;
    bool hitting;

    public Transform ball;
    
    Vector3 aimTargetPosition;

    //Comunicação serial
    SerialPort porta;

    void Start()
    {
        aimTargetPosition = aimTarget.position;

        porta = new SerialPort("/dev/ttyACM0", 115200);
        porta.Open();
        porta.ReadTimeout = -1; //InfiniteTimeout = -1
        porta.DiscardInBuffer();
    }

    // Update is called once per frame
    void Update()
    {

        if(porta.IsOpen){
            try{
                int h = 0;
                int v = 0;
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

                // //verifica se houve modificação nos eixos do player
                // if((h != 0 || v != 0) && !hitting){ //parte do diferente de hitting para não mover a raquete junto ao alvo
                //     // transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //horizontal, para cima/baixo, vertical
                //     transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //horizontal, para cima/baixo, vertical
                // }

                //Debug.Log(porta.ReadByte()); //esse funciona
                //Debug.Log(porta.ReadChar()); //esse funciona também mas vem como byte
                //Debug.Log(porta.ReadLine()); //esse funciona e captura toda a linha corretamente \o/
                //leitura da entrada
                string leitura = porta.ReadLine();
                //calcula e verifica quantidade de caracteres
                int tamanhoLeitura = leitura.Length;
                //captura tempo atual
                // DateTime now = DateTime.Now;
                // //utiliza o tempo em segundos p/ calcular v=a.t
                // int tempo = now.Second;
                
                if(tamanhoLeitura == 4){
                    Debug.Log("Vazio");
                    h=0;
                    v=0;
                }else if(tamanhoLeitura == 6 ){
                    // Debug.Log("Positivo");
                    // Debug.Log(leitura);
                    // //separa aceleração
                    // string[] leituraSep = leitura.Split(';');
                    // // Debug.Log(leituraSep[0]);
                    // string aceleracaoSep = leituraSep[0];
                    // float aceleracao = float.Parse(aceleracaoSep);
                    // float velocidade = aceleracao * tempo;
                    // // Debug.Log(velocidade);
                    // moveAvatar(1, 2.5f);
                    h=1;
                    v=1;
                    if((h != 0 || v != 0) && !hitting){ 
                        moveAvatar(1);
                    }
                }else if(tamanhoLeitura == 7 ){
                    // Debug.Log("Negativo");
                    // Debug.Log(leitura);
                    //separa aceleração
                    // string[] leituraSep = leitura.Split(';');
                    // // Debug.Log(leituraSep[0]);
                    // string aceleracaoSep = leituraSep[0];
                    // float aceleracao = float.Parse(aceleracaoSep);
                    // int arredondar = -1;
                    // float velocidade = arredondar * aceleracao * tempo;
                    // Debug.Log(velocidade);
                    // moveAvatar(0, 2.5f);
                    h=-1;
                    v=-1;
                    if((h != 0 || v != 0) && !hitting){ 
                        moveAvatar(0);
                    }
                }

            }catch(System.Exception){
                throw;
            }
        }

        //captura eixos de movimentação
        // float h = Input.GetAxisRaw("Horizontal"); //no meu caso movimenta apenas na horizontal pois paciente tem pouca mobilidade
        // float v = Input.GetAxisRaw("Vertical");
    }

    //função para mover player
    void moveAvatar(int direcao){
        if(direcao == 1){
            // transform.Translate(-Vector2.right * velocidade * Time.deltaTime, Space.World);
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }

        if(direcao == 0){
            // transform.Translate(Vector2.right * velocidade * Time.deltaTime, Space.World);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    //função para tratar colisões com a raquete -> bola + raquete 
    private void OnTriggerEnter(Collider other) {
        //verifica se a colisão foi com a bola
        if(other.CompareTag("Ball")){
            Vector3 dir = aimTarget.position - transform.position; //pega a posição do alvo para rebater a bolinha - posição atual da raquete
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0); //bolinha

            Vector3 ballDir = ball.position - transform.position;
            aimTarget.position = aimTargetPosition;
        }
    }

}
