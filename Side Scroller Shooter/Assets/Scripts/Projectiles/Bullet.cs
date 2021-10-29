using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //Inspector variables
    [SerializeField] public Vector2 direction = new Vector2(1, 0);
    [SerializeField] public float speed = 20;
    [SerializeField] private float degreeRotation = 0;
    [SerializeField] public float bulletDamage = 1;

    //Instance variables
    private Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * degreeRotation);
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        pos = transform.position;

        pos += (Vector2)transform.right * speed * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
