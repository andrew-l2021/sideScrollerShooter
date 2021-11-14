using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves diagonally until lined up with Player, then only moves horizontally, will follow Player as Player moves.
public class MoveZigZagToPos : MovementBase
{
    //Inspector Variables

    //Instance Variables


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(freezeTime > 0){
            freezeTime -= Time.fixedDeltaTime;
        }else{
            pos = transform.position;
            pos.x -= moveSpeed * Time.deltaTime;
            if(GameObject.Find("Player").transform.position.y > pos.y){
                pos.y += moveSpeed * Time.deltaTime;
            }else{
                pos.y -= moveSpeed * Time.deltaTime;
            }
            transform.position = pos;
        }
        
    }
}
