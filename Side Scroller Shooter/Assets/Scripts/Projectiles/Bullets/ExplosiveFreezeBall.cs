using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveFreezeBall : MonoBehaviour
{
    //Inspector variables
    [Header("Explosion")]
    [SerializeField] private GameObject freezeExplosion;
    [SerializeField] private float timeToExplosion = 2;
    [SerializeField] private float numberOfExplosionsPerLayer = 5;
    [SerializeField] private int numberOfLayers = 5;
    [SerializeField] private float distanceBetweenLayers = 1;
    [SerializeField] private float timeBetweenLayers = 0.25F;
    [Header("Movement Parameters")]
    [SerializeField] private float initialSpeed = 1.0F;
    [SerializeField] private float acceleration = 1.0F;
    [SerializeField] private float jerk = 1.0F; //derivative of acceleration
    

    //Instance variables
    private float speed = 0;
    private Vector2 pos;
    public float damagePercentage { get; set; } = 1; //set by Blaster classes
    private bool exploded = false;

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
        if(!exploded){
            if (collision.tag == "Enemy")
            {
                StartCoroutine(explode());
            }
            exploded = true;
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
    
    IEnumerator explode()
    {
        GetComponent<Renderer>().enabled = false;
        GameObject explosionMiddleObject = Instantiate(freezeExplosion, pos, Quaternion.identity);
        ExplosiveFreeze explosivMiddleFreezeComponent = explosionMiddleObject.GetComponent<ExplosiveFreeze>();
        explosivMiddleFreezeComponent.explosionDamage *= damagePercentage;
        yield return new WaitForSeconds(timeBetweenLayers);

        for(int j = 1; j < numberOfLayers + 1; j++){
            for (int i = 0; i < numberOfExplosionsPerLayer; i++){
                Vector2 explosivePos = new Vector2(j * distanceBetweenLayers * Mathf.Cos(2*Mathf.PI*(i/numberOfExplosionsPerLayer)), j * distanceBetweenLayers * Mathf.Sin(2*Mathf.PI*(i/numberOfExplosionsPerLayer)));
                GameObject explosionObject = Instantiate(freezeExplosion, pos + explosivePos, Quaternion.identity);
                ExplosiveFreeze explosiveFreezeComponent = explosionObject.GetComponent<ExplosiveFreeze>();
                explosiveFreezeComponent.explosionDamage *= damagePercentage;
            }
            yield return new WaitForSeconds(timeBetweenLayers);
        }
        
        Destroy(gameObject);
    }
}
