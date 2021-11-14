using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStandard : MonoBehaviour
{

    //Inspector Variables
    [Header("Health Parameters")]
    [SerializeField] private float startingHealth;
    [Header("Automatic Death Parameters")]
    [SerializeField] public bool timedDeath = false;
    [SerializeField] public float lifetime = 5;

    //Instance Variables
    bool canBeDestroyed = false;
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
        if (pos.x < -15 || lifetime < 0){
            Destroy(gameObject);
        }
        //destroy object if health drops to or below 0
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            Destroy(gameObject);
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
            Debug.Log("Enemy Health: " + currentHealth);

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
}
