using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Vector3 initialPos;
    
    public string hitter;

    int playerScore;
    int botScore;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text botScoreText;

    public bool playing = true;

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
            if(playing){
                if(hitter == "player"){
                    playerScore++;
                }else if(hitter == "bot"){
                    botScore++;
                }
                playing = false;
                UpdateScores();
            }
            
        }else if(collision.transform.CompareTag("Net")){
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            // GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.position = initialPos;

            // GameObject.Find("player").GetComponent<Player>().Reset();
            if(playing){
                if(hitter == "player"){
                    botScore++;
                }else if(hitter == "bot"){
                    playerScore++;
                }
                playing = false;
                UpdateScores(); 
            }
            
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Out") && playing){
            if(hitter == "player"){
                playerScore++;
            }else if(hitter == "bot"){
                botScore++;
            }
            playing = false;
            UpdateScores();
        }
    }

    void UpdateScores(){
        playerScoreText.text = playerScore.ToString();
        botScoreText.text = botScore.ToString();
    }

}
