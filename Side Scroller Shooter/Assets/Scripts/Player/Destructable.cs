using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    bool canBeDestroyed = false;

    [Header("Enemy Health")]
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private int damage = 1;
    
    //timer
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(timer > 0)
        {
            print("damage boost!");
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //when the the timer is up end the damage boost
                damage = 1;
            }
        }

        if (transform.position.x < 7.8f)
        {
            canBeDestroyed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth); //1 refers to basic bullet damage to enemies
            Debug.Log("Enemy Health: " + currentHealth);

            if (!canBeDestroyed)
            {
                return;
            }
            Bullet bullet = collision.GetComponent<Bullet>();
            Destroy(bullet.gameObject);
            if (currentHealth <= 0 && !dead)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TemporarilyIncreaseDamage(float val, float time)
    {
        damage = (int) val;
        timer = time;
    }
}
