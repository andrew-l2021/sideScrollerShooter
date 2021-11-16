using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBall : MonoBehaviour
{
    //Inspector variables
    [SerializeField] private GameObject explosion;
    [SerializeField] private float timeToExplosion = 2;
    [SerializeField] private float initialSpeed = 1.0F;
    [SerializeField] private float acceleration = 1.0F;
    [SerializeField] private float jerk = 1.0F; //derivative of acceleration
    [SerializeField] private Vector2 explosionOffset = new Vector2(0, 0.87F);
    

    //Instance variables
    private float speed = 0;
    private Vector2 pos;
    public float damagePercentage { get; set; } = 1; //set by Blaster classes

    // Start is called before the first frame update
    void Start()
    {
        speed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToExplosion < 0){
            explode();
        }
        if(pos.x > 8.3f){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            explode();
        }
    }

    private void FixedUpdate()
    {
        timeToExplosion -= Time.fixedDeltaTime;
        pos = transform.position;
        acceleration += jerk * Time.fixedDeltaTime;
        speed += acceleration * Time.fixedDeltaTime;
        pos += (Vector2)transform.right * speed * Time.fixedDeltaTime;
        transform.position = pos;
    }

    private void explode(){
        GameObject explosionObject = Instantiate(explosion, (Vector2)transform.position + explosionOffset, Quaternion.identity);
        Debug.Log("Damage Percentage = " + damagePercentage);
        Debug.Log("Total Damage = " + explosionObject.GetComponent<Explosive>().explosionDamage * damagePercentage);
        explosionObject.GetComponent<Explosive>().explosionDamage *= damagePercentage;
        Destroy(gameObject);
    }
}
