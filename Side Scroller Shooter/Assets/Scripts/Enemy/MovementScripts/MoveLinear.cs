using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLinear : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] float moveSpeed = 5;

    //Instance Variables
    Rigidbody2D enemyComponent;

    // Start is called before the first frame update
    void Start()
    {
        enemyComponent = GetComponent<Rigidbody2D>();
        enemyComponent.velocity = -transform.right * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
