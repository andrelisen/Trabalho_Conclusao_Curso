using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Vector3 initialPos; //posicao inicial da bolinha

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;//captura posicao inicial da bolinha
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.transform.CompareTag("Wall")){
            GetComponent<Rigidbody>().velocity = Vector3.zero; 
            //coloca bola na posição inicial
            transform.position = initialPos;
        }
    }

}