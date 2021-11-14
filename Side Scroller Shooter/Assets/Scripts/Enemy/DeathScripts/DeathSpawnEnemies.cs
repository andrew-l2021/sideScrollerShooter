using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpawnEnemies : MonoBehaviour
{

    bool canBeDestroyed = false;

    //Inspector variables
    [Header("Enemy Health")]
    [SerializeField] private float startingHealth;
    [Header("Automatic Death")]
    [SerializeField] public bool timedDeath = false;
    [SerializeField] public float lifetime = 5;
    [Header("Enemy Spawn on Death")]
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private Vector2[] enemiesPosition;
    [SerializeField] private bool childPositionRelativeToParent = true;
    [SerializeField] private float[] enemiesDegreeRotation;

    //Instance Variables
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private Vector2 pos;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        if (pos.x < 7.8f)
        {
            canBeDestroyed = true;
        }
        if (pos.x < -15){
            Destroy(gameObject);
        }
        //destroy object if lifetime hits 0 and spawn children enemies
        if (lifetime < 0){
            spawnChildren();
        }
        //destroy object if health drops to or below 0 and spawn children enemies
        if (currentHealth <= 0 && !dead)
        {
            spawnChildren();
        }
    }

    private void FixedUpdate(){
        if(timedDeath){
            lifetime -= Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            //subtract bulletDamage from the currentHealth of the Enemy
            Bullet bullet = collision.GetComponent<Bullet>();
            currentHealth = Mathf.Clamp(currentHealth - bullet.bulletDamage, 0, startingHealth);
            //Debug.Log("Enemy Health: " + currentHealth);

            //ignore destroy code if Enemy cannot be destroyed
            if (!canBeDestroyed)
            {
                return;
            }

            //destroy Bullet object
            Destroy(bullet.gameObject);
            
        }
        if (collision.tag == "Explosion")
        {
            Explosive explosive = collision.GetComponent<Explosive>();
            currentHealth = Mathf.Clamp(currentHealth - explosive.explosionDamage, 0, startingHealth);
        }
        if (collision.tag == "ExplosiveFreeze")
        {
            ExplosiveFreeze explosiveFreeze = collision.GetComponent<ExplosiveFreeze>();
            currentHealth = Mathf.Clamp(currentHealth - explosiveFreeze.explosionDamage, 0, startingHealth);
            Debug.Log(explosiveFreeze.freezeTime);
            gameObject.GetComponent<MovementBase>().addFreezeTime(explosiveFreeze.freezeTime);
        }
    }
    
    private void spawnChildren()
    {
        dead = true;
        if(childPositionRelativeToParent){
            for(int j = 0; j < enemiesPosition.Length; j++){
                enemiesPosition[j] += (Vector2)transform.position;
            }
        }
        for (int i = 0; i < enemiesToSpawn.Length; i++){
            Instantiate(enemiesToSpawn[i], enemiesPosition[i], Quaternion.Euler(Vector3.forward * enemiesDegreeRotation[i]));
        }
        Destroy(gameObject);
    }
}
