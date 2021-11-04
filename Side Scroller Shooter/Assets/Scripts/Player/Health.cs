using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    /*[Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;*/

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        //spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0 && !dead)
        {
            GetComponent<Player>().enabled = false;
            dead = true;
        }

        /*if (currentHealth > 0) //Invulnerability time frame implementation
        {
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                GetComponent<Player>().enabled = false;
                dead = true;
            }
        }*/
    }

    public void IncreaseMaxHealth(float healthAdded)
    {
        //currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        startingHealth += healthAdded;
    }

    /*private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }*/
}
