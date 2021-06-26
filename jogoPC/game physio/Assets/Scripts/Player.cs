using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class Player : MonoBehaviour
{
    public Transform aimTarget; //alvo para onde a bolinha será lançada para o lado do bot
    float speed = 7.5f; //velocidade da raquete que será multiplicada pela posição
    float force = 20;
    bool hitting;

    public Transform ball;
    
    Vector3 aimTargetPosition;

    //parte da comunicação serial 
    public SerialPort sp;
    public Thread serialThread;
    float x_mapeado;
    float angleX_filtrado;
    float y_mapeado;
    float angley_filtrado;

    // Start is called before the first frame update
    void Start()
    {
        aimTargetPosition = aimTarget.position;

        //parte da comunicação serial
        print("app iniciando….");
        x_mapeado = 0;
        angleX_filtrado =0;
        angley_filtrado = 0;
        sp = new SerialPort("COM3", 9600);
        sp.Open();
        sp.ReadTimeout = 50;
        sp.Handshake = Handshake.None;
        serialThread = new Thread(recData);
        serialThread.Start();
        print("app iniciado!");
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

    void recData()
    {
       string data;
        while (1 == 1)
        {
            if ((sp != null) && (sp.IsOpen))
            {
                try
                {
                    data = sp.ReadLine();
                    string[] tokens = data.Split(',');
                    // parse da string recebida do arduino ex: "125,592,8"
                    int eixo_x = System.Convert.ToInt16(tokens[0]);
                    int eixo_y = System.Convert.ToInt16(tokens[1]);
                    int eixo_z = System.Convert.ToInt16(tokens[2]);

                    float vx =  (float) eixo_x * 5f / 1023f;
                    float vy = (float)eixo_y * 5f / 1023f;
                    float vz = (float)eixo_z * 5f / 1023f;

                    float z = 1.8f * 5.1f / 5f; //1.8 corresponde a 0G do acelerometro
                    vx -= z;
                    vy -= z;
                    vz -= z;

                    float sensitivity = 200 * 5.1f / 5f; // sensibilidade
                    sensitivity = sensitivity / 1000;
                    float gx = vx * sensitivity;
                    float gy = vy * sensitivity;
                    float gz = vz * sensitivity;


                    // calculo do angulo
                    float m = (float)System.Math.Sqrt (gx * gx + gy * gy + gz * gz);
                    float angleX = (float)System.Math.Acos(gx / m);
                    float angleY = (float)System.Math.Acos(gy / m);
                    float angleZ = (float)System.Math.Acos(gz / m);

                    // converte angulo para graus
                    angleX = angleX * 180f / 3.14f;
                    angleY = angleY * 180f / 3.14f;
                    angleZ = angleZ * 180f / 3.14f;

                    // filtro passa-baixa
                    float alpha = 0.1f;
                    angleX_filtrado = angleX * alpha + (angleX_filtrado * (1.0f – alpha));
                    angley_filtrado = angleY * alpha + (angley_filtrado * (1.0f – alpha));


                    //normalizar valores
                    x_mapeado = mapear(angleX_filtrado, 20, 166, 90, –90);
                    y_mapeado = mapear(angley_filtrado, 15, 160, 90, –90);

                    //Debug.Log(angleX + "," + x_mapeado);
                    //Debug.Log(angleX + "," + angleY + "," + angleZ);
                    Debug.Log(vx + "," + vy + "," + vz); //valor raw dos sensores
                    // Debug.Log(tokens[0] + "," + tokens[1] + "," + tokens[2]);
                }
                catch (System.TimeoutException)
                {
                    data = null;
                }
                //print(data);
                
            }
            Thread.Sleep(1);
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
