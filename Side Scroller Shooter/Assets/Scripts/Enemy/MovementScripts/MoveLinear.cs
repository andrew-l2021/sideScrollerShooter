using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLinear : MovementBase
{
    //Inspector Variables

    //Instance Variables
    Rigidbody2D enemyComponent;

    // Start is called before the first frame update
    void Start()
    {
        enemyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(freezeTime > 0){
            freezeTime -= Time.deltaTime;
            enemyComponent.velocity = Vector2.zero;
        }else{
            enemyComponent.velocity = -transform.right * moveSpeed;
        }
    }

}
