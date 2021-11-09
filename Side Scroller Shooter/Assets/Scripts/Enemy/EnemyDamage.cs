using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.tag == "Shield")
        {
            collision.GetComponent<Shield>().DestroyShield();
            Destroy(gameObject);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
