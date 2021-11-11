using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHoming : MovementBase
{
    //Inspector Variables

    //Instance variables
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(freezeTime > 0){
            freezeTime -= Time.deltaTime;
        }else{
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
