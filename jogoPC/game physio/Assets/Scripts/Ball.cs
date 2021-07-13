using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 initialPos;
    
    public string hitter;

    int playerScore;
    int botScore;

    void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.CompareTag("Wall")){
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            // GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.position = initialPos;

            // GameObject.Find("player").GetComponent<Player>().Reset();

            if(hitter == "player"){
                playerScore++;
            }else if(hitter == "bot"){
                botScore++;
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Out")){
            if(hitter == "player"){
                botScore++;
            }else if(hitter == "bot"){
                playerScore++;
            }
        }
    }

}
