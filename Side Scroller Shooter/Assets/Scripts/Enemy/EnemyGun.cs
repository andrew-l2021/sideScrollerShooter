using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    //Inspector Variables
    public GameObject bullet;
    public GameObject bulletParent;
    [SerializeField] private float fireRate;
    [SerializeField] private float startDelay;

    //Instance Variables
    private float nextFireTime;

    // Update is called once per frame
    void Update()
    {
        if (nextFireTime < Time.time && bulletParent.transform.position.x > -10 && Time.time > startDelay)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
