using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move in a straight line that intercepts the location of Player at spawn time, does not change trajectory if Player moves.
public class MoveLinearToPos : MonoBehaviour
{
    Vector2 targetLocation;
    Vector2 pos;
    float slopeToPlayer;
    [SerializeField] float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        targetLocation = GameObject.Find("Player").transform.position;
        slopeToPlayer = (targetLocation.y - pos.y)/(targetLocation.x - pos.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {        
        pos = transform.position;

        pos.y -= slopeToPlayer * moveSpeed * Time.deltaTime;
        pos.x -= moveSpeed * Time.deltaTime;

        if (pos.x < -15)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
