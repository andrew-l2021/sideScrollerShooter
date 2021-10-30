using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff : MonoBehaviour
{
    [SerializeField] private GameObject shield;

    private void Start()
    {
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shield.GetComponent<Shield>().ActivateShield();
            Destroy(gameObject); //Will destroy regardless of whether there is a previous shield up or not
        }
    }
}
