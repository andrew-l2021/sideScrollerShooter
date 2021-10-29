using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStandard : MonoBehaviour
{
    

    //Inspector Variables
    [Header("Enemy Health")]
    [SerializeField] private float startingHealth;
    [Header("Automatic Death")]
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
            currentHealth = Mathf.Clamp(currentHealth - 1, 0, startingHealth); //1 refers to basic bullet damage to enemies
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
}
