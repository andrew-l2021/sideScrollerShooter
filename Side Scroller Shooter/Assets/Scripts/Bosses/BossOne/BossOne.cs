using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossOne : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] protected int points;
    [SerializeField] protected CircleCollider2D colliderBounds;

    //Instance Variables
    protected BossMasterClass bossMasterClass;
    protected BossProjectileSpawner bossProjectileSpawner;
    protected Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        bossMasterClass = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossMasterClass>();
        bossProjectileSpawner = gameObject.GetComponent<BossProjectileSpawner>();
        colliderBounds = GetComponent<CircleCollider2D>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            //ignore destroy code if Enemy cannot be destroyed
            pos = transform.position;
            if (pos.x > 10)
            {
                return;
            }

            //subtract bulletDamage from the currentHealth of the Enemy
            Bullet bullet = collision.GetComponent<Bullet>();
            float damageToBeDone = bullet.bulletDamage;
            Destroy(bullet.gameObject);
            bossMasterClass.decrementHealth(damageToBeDone);
        }
    }
}
