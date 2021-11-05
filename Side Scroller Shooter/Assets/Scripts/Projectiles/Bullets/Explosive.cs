using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    //Inspector variables
    [SerializeField] public float explosionDamage = 5;

    //Instance variables

    void ExplosionDone()
    {
        Destroy(gameObject);
    }
}
