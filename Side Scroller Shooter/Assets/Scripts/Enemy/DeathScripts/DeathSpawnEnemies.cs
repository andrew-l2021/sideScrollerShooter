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
    }
}
