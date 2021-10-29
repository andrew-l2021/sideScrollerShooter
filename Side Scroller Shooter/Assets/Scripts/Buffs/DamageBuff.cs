using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
    [SerializeField] protected float damagePercentBuff;
    [SerializeField] protected int damageTime;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TemporarilyIncreaseDamage(damagePercentBuff, damageTime);
            Destroy(gameObject);
        }

    }
    
}
