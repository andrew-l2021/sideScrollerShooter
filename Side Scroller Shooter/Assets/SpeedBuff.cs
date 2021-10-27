using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float speedBuff;
    [SerializeField] protected int buffTime;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered");
        if (collision.tag == "Player")
        {
            print("adding " + speedBuff + " speed to player for " + buffTime);
            collision.GetComponent<Player>().TemporarilyIncreaseSpeed(speedBuff, buffTime);
            Destroy(gameObject);
        }
    }
}
