using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class Player : MonoBehaviour
{
    public Transform aimTarget; //alvo para onde a bolinha será lançada para o lado do bot
    
    float speed = 20.0f; //velocidade da raquete que será multiplicada pela posição
    float force = 15; //15
    
    bool hitting;

    public Transform ball;
    //public Rigidbody playerRb;

    Vector3 aimTargetPosition;

    //posição aleatoria de rebate da bolinha 
    public Transform[] targets;

    //Comunicação serial
    // SerialPort porta;

    public static bool flagDificuldade;

    public static int numAcertos;

    public static float acelMedia;
    public static int numLeitura;
    public static float tempoDecorrido;

    int testeSBlue;
    void Start()
    {
        //playerRb = this.GetComponent<Rigidbody>();
        aimTargetPosition = aimTarget.position;
        // porta = new SerialPort("/dev/ttyACM0", 115200);
        // porta.Open();
        // porta.ReadTimeout = -1; //InfiniteTimeout = -1
        // porta.DiscardInBuffer();
    }

    void FixedUpdate()
    {
        if(configCalibragem.porta.IsOpen){
            try{
                 if(configCalibragem.porta.BytesToRead == 0){ //Não está sendo recebido dados no buffer
                     //Debug.Log(porta.BytesToRead);
                      Debug.Log("Sem recebimento de dados B!");
                 }else{
                    int dadoNoSensor = configCalibragem.porta.ReadByte();
                     configCalibragem.porta.DiscardInBuffer();
                     // if(dadoNoSensor == 68 || dadoNoSensor == 69 || dadoNoSensor == 80){ //se for dados de movimento
                     Debug.Log("Recebimento de dados de MOVIMENTAÇÃO!");
                     configCalibragem.porta.Write("2");
                     //     //moveAvatar(dadoNoSensor);
                         if(dadoNoSensor == 68){ //direita
                             Debug.Log("Direita");
                             transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime);
                         }else if(dadoNoSensor == 69){ //esquerda
                             Debug.Log("Esquerda");
                             transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
                         }else if(dadoNoSensor == 80){ //parado
                             Debug.Log("Parado");
                             transform.position += new Vector3(0, 0, 0 * speed * Time.deltaTime);
                         }
                     // }
                 }
             }catch(System.Exception){
                 throw;
             }
         }    
    }

    //função para mover o avatar usando como entrada de dados o nó sensor
    void moveAvatar(int direcao){
        if(direcao == 68){ //direita
          Debug.Log("Direita");
          transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime);
        }else if(direcao == 69){ //esquerda
            Debug.Log("Esquerda");
            transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
        }else if(direcao == 80){ //parado
            Debug.Log("Parado");
            transform.position += new Vector3(0, 0, 0 * speed * Time.deltaTime);
        }
    }

    //função para mover o avatar usando como entrada de dados o teclado
    void movimentaUsandoTeclado(){

        
        //capturar posição se pra frente, pra trás, pra direita ou pra esquerda
        float h = Input.GetAxisRaw("Horizontal"); //direita = 1 esquerda = -1
        float v = Input.GetAxisRaw("Vertical");

        if(h != 0 || v != 0){ //movimenta avatar
            //time.deltatime evita alta taxa de atualização de quadros
            // transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //se não colocar uma velocidade o movimento do player é muito rapidamente
            // playerRb.velocity = new Vector3(0, 0, h) * speed;
            // Debug.Log(Time.deltaTime);
            if(h == 1f){ //direita
                Debug.Log("Posição Atual D = ");
                Debug.Log(transform.position.z);
                transform.position += new Vector3(0, 0, 1 * speed * Time.deltaTime); //Time.deltatime = fazer o movimento c/ velocidade constante - retorna 0.02
                Debug.Log("Posição Depois D = ");
                Debug.Log(transform.position.z);
            }else{ //esquerda
                Debug.Log("Posição Atual E = ");
                Debug.Log(transform.position.z);
                transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
                Debug.Log("Posição Depois E = ");
                Debug.Log(transform.position.z);
            }
        }
    }

    //função para retornar aleatoriamente uma posição p/ rebate da bolinha pelo player - 3 opções por enquanto
    Vector3 PickTarget(){
        int randomValue =  Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }

    //função para tratar colisões com a raquete -> bola + raquete 
    // private void OnCollisionEnter(Collision other){
    private void OnTriggerEnter(Collider other) {
        //verifica se a colisão foi com a bola
        if(other.CompareTag("Ball")){
            numAcertos++;
            Vector3 dir;
            //movimentando target usando teclado
            if(flagDificuldade == false){
                dir = aimTarget.position - transform.position; //pega a posição do alvo para rebater a bolinha - posição atual da raquete
            }else{
                //movimentando target usando aleatoriedade
                dir = PickTarget() - transform.position; //pega a posição do alvo para rebater a bolinha - posição atual da raquete
            }
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0); //bolinha

            Vector3 ballDir = ball.position - transform.position;
            aimTarget.position = aimTargetPosition;

            ball.GetComponent<Ball>().hitter = "player"; //modificando uma var. publica da class ball
            ball.GetComponent<Ball>().playing = true;
        }
    }

    
    //seta posição quando erra jogada
    // public void Reset(){
    //     if(servedRight){
    //         transform.position = serveLeft.position;
    //     }else{
    //         transform.position = serveRight.position;
    //     }

    //     servedRight = !servedRight;

    // }

}
