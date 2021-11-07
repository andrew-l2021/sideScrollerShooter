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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 7.8f)
        {
            canBeDestroyed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            currentHealth = Mathf.Clamp(currentHealth - 1, 0, startingHealth); //1 refers to basic bullet damage to enemies
            //Debug.Log("Enemy Health: " + currentHealth);

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
