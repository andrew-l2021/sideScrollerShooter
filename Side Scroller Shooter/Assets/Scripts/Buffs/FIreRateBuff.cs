using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBuff : MonoBehaviour
{
    [SerializeField] protected float fireRateBuff;
    [SerializeField] protected int fireRateTime;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TemporarilyIncreaseFireRate(fireRateBuff, fireRateTime);
            Destroy(gameObject);
        }
    }
}
