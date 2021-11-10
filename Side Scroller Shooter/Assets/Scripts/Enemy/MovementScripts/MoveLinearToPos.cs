using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move in a straight line that intercepts the location of Player at spawn time, does not change trajectory if Player moves.
public class MoveLinearToPos : MonoBehaviour
{
    Vector2 targetLocation;
    Vector2 pos;
    [SerializeField] float moveSpeed = 5;
    Rigidbody2D enemyComponent;

    // Start is called before the first frame update
    void Start()
    {
        //getting position, calculating and normalizing velocity vector based on Player position and Enemy spawn position, applying velocity to Rigidbody2D
        enemyComponent = GetComponent<Rigidbody2D>();
        pos = transform.position;
        targetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyComponent.velocity = (targetLocation - pos).normalized * moveSpeed;

        //Turn towards player
        var offset = 90f;
        Vector2 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
