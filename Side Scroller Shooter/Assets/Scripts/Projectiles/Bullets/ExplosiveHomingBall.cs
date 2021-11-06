using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveHomingBall : MonoBehaviour
{
    //Inspector variables
    [SerializeField] private GameObject explosion;
    [SerializeField] private float initialSpeed = 1.0F;
    [SerializeField] private float acceleration = 1.0F;
    [SerializeField] private float jerk = 1.0F; //derivative of acceleration
    [SerializeField] private float idleTime = 1;
    [SerializeField] private float lifetime = 5;
    
    //Instance variables
    private float speed = 0;
    public float damagePercentage { get; set; } = 1; //set by Blaster classes
    private float timer = 0;

    float distanceToClosestEnemy;
    float distanceToEnemy;
    GameObject closestEnemy = null;
    GameObject[] allEnemies;
    
    Player playerGameObject;
    Vector2 posFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        speed = initialSpeed;
        playerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        posFromPlayer = (transform.position - playerGameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > idleTime){
            distanceToClosestEnemy = Mathf.Infinity;
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(GameObject currentEnemy in allEnemies){
                distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                if(distanceToEnemy < distanceToClosestEnemy){
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }
        if(timer > lifetime)
        {
            explode();
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
        if(timer < idleTime){
            transform.position = (Vector2)playerGameObject.transform.position + posFromPlayer;
        }
        else{
            acceleration += jerk * Time.fixedDeltaTime;
            speed += acceleration * Time.fixedDeltaTime;
        }
        if(closestEnemy != null){
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, Time.fixedDeltaTime * speed);
        }
    }

    private void explode(){
        GameObject explosionObject = Instantiate(explosion, transform.position, Quaternion.identity);
        Debug.Log("Damage Percentage = " + damagePercentage);
        Debug.Log("Total Damage = " + explosionObject.GetComponent<Explosive>().explosionDamage * damagePercentage);
        explosionObject.GetComponent<Explosive>().explosionDamage *= damagePercentage;
        Destroy(gameObject);
    }
}
