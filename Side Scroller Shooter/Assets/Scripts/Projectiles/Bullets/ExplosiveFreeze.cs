using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveFreeze : MonoBehaviour
{
    //Inspector variables
    [SerializeField] public float explosionDamage = 3;
    [SerializeField] public float freezeTime = 3;

    //Instance variables

    void ExplosionDone()
    {
        Destroy(gameObject);
    }
}
