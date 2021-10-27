using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIreRateBuff : MonoBehaviour
{
    [SerializeField] protected float fireRateBuff;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered");
        if (collision.tag == "Player")
        {
            print("adding " + fireRateBuff + " firerate to player");
            collision.GetComponent<Player>().IncreaseFireRate(fireRateBuff);
            Destroy(gameObject);
        }
    }
}
