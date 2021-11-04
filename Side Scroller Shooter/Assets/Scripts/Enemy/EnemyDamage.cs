using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    public GameObject player;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
            player.GetComponent<Player>().timeLastQBarChange = player.GetComponent<Player>().timer;
            player.GetComponent<Player>().timeLastWBarChange = player.GetComponent<Player>().timer;
            player.GetComponent<Player>().timeLastEBarChange = player.GetComponent<Player>().timer;
            player.GetComponent<Player>().currentQRate = player.GetComponent<Player>().maxQRate / 2; //Resets regeneration acceleration
            player.GetComponent<Player>().currentWRate = player.GetComponent<Player>().maxWRate / 2;
            player.GetComponent<Player>().currentERate = player.GetComponent<Player>().maxERate / 2;
        }
        if (collision.tag == "Shield")
        {
            collision.GetComponent<Shield>().DestroyShield();
            Destroy(gameObject);
        }
    }
}
