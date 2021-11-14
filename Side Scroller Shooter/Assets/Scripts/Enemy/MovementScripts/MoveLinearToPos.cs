using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move in a straight line that intercepts the location of Player at spawn time, does not change trajectory if Player moves.
public class MoveLinearToPos : MovementBase
{
    //Inspector Variables

    //Instance Variables
    Vector2 targetLocation;
    Rigidbody2D enemyComponent;

    // Start is called before the first frame update
    void Start()
    {
        //getting position, calculating and normalizing velocity vector based on Player position and Enemy spawn position, applying velocity to Rigidbody2D
        enemyComponent = GetComponent<Rigidbody2D>();
        pos = transform.position;
        targetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyComponent.velocity = (targetLocation - pos).normalized * moveSpeed;

        Vector2 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        direction.Normalize();
        var offset = 90f;
        //Turn towards player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    // Update is called once per frame
    void Update()
    {
        if(freezeTime > 0){
            freezeTime -= Time.deltaTime;
            enemyComponent.velocity = Vector2.zero;
        }else{
            enemyComponent.velocity = (targetLocation - pos).normalized * moveSpeed;
        }
    }

}
