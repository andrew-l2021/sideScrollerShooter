using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] protected float healthAdded;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered");
        if (collision.tag == "Player")
        {
            print("adding " + healthAdded + " health to player");
            collision.GetComponent<Health>().AddHealth(healthAdded);
            Destroy(gameObject);
        }
    }
}
