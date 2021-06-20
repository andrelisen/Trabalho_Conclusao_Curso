using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20.0f; //velocidade de movimentação do player
    private Vector3 pos; //posição do player no plano 3d
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position; //capturando a posição do player 
        //incrementando as posições em x e z (hor, ver) para movimentar o player
        pos.x = pos.x + moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.z = pos.z + moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position = pos;
        /*
            Concluída a parte acima, ir em edit -> project settings -> input manager -> verificou os btns 
            de manuseiro da horizontal e vertical
        */
    }
}
