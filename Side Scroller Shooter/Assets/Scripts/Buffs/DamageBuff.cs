using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
    [SerializeField] protected int damageBuff;
    [SerializeField] protected int damageTime;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered");
        if (collision.tag == "Player")
        {
            print("adding " + damageBuff + " firerate to player for " + damageTime);
            collision.GetComponent<Destructable>().TemporarilyIncreaseDamage(damageBuff, damageTime);
            Destroy(gameObject);
        }

    }
    
}
