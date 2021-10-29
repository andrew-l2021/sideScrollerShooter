using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHoming : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
