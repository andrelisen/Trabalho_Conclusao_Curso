using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class movimentarBolinha : MonoBehaviour
{
    public float velocidade;
    SerialPort porta;

    //movimentando a bolinha 
    float speed = 10F;
    //Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        porta = new SerialPort("/dev/ttyACM0", 9600);
        porta.Open();
        porta.ReadTimeout = 5000; //delay para a porta

    }

    // Update is called once per frame
    void Update()
    {
        //escreve msg para arduino
        // porta.Write("1");   
    
        if(porta.IsOpen){
            try{
                //Debug.Log(porta.ReadByte()); //esse funciona
                //Debug.Log(porta.ReadChar()); //esse funciona também mas vem como byte
                Debug.Log(porta.ReadLine()); //esse funciona e captura toda a linha corretamente \o/

            }catch(System.Exception){
                Debug.Log("Ocorreu um erro!");
            }
            
        }
    }
}
