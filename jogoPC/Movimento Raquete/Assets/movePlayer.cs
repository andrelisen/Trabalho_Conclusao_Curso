using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class movePlayer : MonoBehaviour
{
    //declração da porta serial de comunicação com o arduino
    SerialPort porta;
    //declaração do elemento rigidbody que será utilizado para movimentar o player
    Rigidbody rigid;

    float speed = 0.005f; //velocidade ficticia usando btns
    // Start is called before the first frame update
    void Start()
    {
        porta = new SerialPort("/dev/ttyACM0", 115200);
        porta.Open();
        porta.ReadTimeout = 5000; //delay para a porta
        //captura rigidbody do player
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Parte utilizando os sensores
        //Escreve mensagem para o arduino
        // porta.Write("1");   
        //Parte que recebe valores do Arduino vindo dos sensores
        if(porta.IsOpen){
            try{
                //Debug.Log(porta.ReadByte()); //esse funciona
                //Debug.Log(porta.ReadChar()); //esse funciona também mas vem como byte
                //Debug.Log(porta.ReadLine()); //esse funciona e captura toda a linha corretamente \o/
                
                //lendo os valores recebidos via comun serial
                string leitura = porta.ReadLine();
                //realiza a separação do valor da aceleração do ;
                string[] separacao = leitura.Split(';');
                //recebe o valor da aceleração
                string valorAceleracao = separacao[0];
                // Debug.Log(valorAceleracao);
                //converte a string da aceleração em double 
                float aceleracao = float.Parse(valorAceleracao);
                // double aceleracao = System.Convert.ToDouble(valorAceleracao);
                // Debug.Log(aceleracao);
                print(aceleracao);
                // transform.Translate(new Vector2(aceleracao, 0) * speed * Time.deltaTime);
                transform.position = new Vector2(aceleracao * 1f * Time.deltaTime, 0);

                // rigid.AddForce (new Vector2(aceleracao, 0) * 0.5f * Time.deltaTime);
                
            }catch(System.Exception){
               Debug.Log("Erro");

            }
            
        }

        //USANDO AS SETAS DO TECLADO PARA TESTE

        //captura eixos de movimentação
        // float h = Input.GetAxisRaw("Horizontal"); //no meu caso movimenta apenas na horizontal pois paciente tem pouca mobilidade
        // float v = Input.GetAxisRaw("Vertical");

        // if((h != 0 || v != 0)){ //parte do diferente de hitting para não mover a raquete junto ao alvo
        //     // transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); //horizontal, para cima/baixo, vertical
        //     transform.Translate(new Vector2(h, 0) * speed * Time.deltaTime); //horizontal, para cima/baixo, vertical
        // }
    }
}
