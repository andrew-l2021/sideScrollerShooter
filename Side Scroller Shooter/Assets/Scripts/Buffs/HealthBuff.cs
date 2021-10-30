using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] protected float healthAdded;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().IncreaseMaxHealth(healthAdded);
            Destroy(gameObject);
        }
    }
}
