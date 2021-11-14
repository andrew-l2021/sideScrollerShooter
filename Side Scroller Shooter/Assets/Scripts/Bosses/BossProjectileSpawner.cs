using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileSpawner : MonoBehaviour
{
    //Inspector Variables
    public GameObject[] projectiles;
    public GameObject[] bulletParents;

    /* CUSTOM ATTRIBUTES */
    [HideInInspector] public bool activelyFiring;
    [HideInInspector] public float fireRate;

    [HideInInspector] public int numberOfProjectilesPerBurst; //Can fire more than one projectile at a time if set greater than 1

    [HideInInspector] public bool randomFire; //Random fire rate (ignores fireRate variable above)
    [HideInInspector] public float randomFireMinimumTime;
    [HideInInspector] public float randomFireMaximumTime;

    [HideInInspector] public int projectileNumber; //Is accessed by a specific boss class that chooses what projectile to shoot based on the boss phase

    [HideInInspector] public bool moreThanOneProjectile; //Equals true if the boss fires more than one type of projectile during one phase
    [HideInInspector] public int[] projectileList; //List of indexes of projectiles to choose from if variable "moreThanOneProjectile" is true
    [HideInInspector] public int[] burstList; //Numbers of projectiles per burst for each projectile in projectileList

    //Instance Variables
    private float nextFireTime;
    private int bulletParentNumber;

    // Update is called once per frame
    void Update()
    {
        if (nextFireTime < Time.time && activelyFiring)
        {
            //Fires projectiles
            StartCoroutine(ExecuteFire());

            //Resets fire rate
            if (randomFire)
            {
                nextFireTime = Time.time + Random.Range(randomFireMinimumTime, randomFireMaximumTime);
            }
            else
            {
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    IEnumerator ExecuteFire()
    {
        if (!moreThanOneProjectile)
        {
            for (int burstNumber = 0; burstNumber < numberOfProjectilesPerBurst; burstNumber++) //Loops through number of bursts
            {
                for (bulletParentNumber = 0; bulletParentNumber < bulletParents.Length; bulletParentNumber++) //Loops through all the boss's projectile origins
                {
                    Instantiate(projectiles[projectileNumber], bulletParents[bulletParentNumber].transform.position, Quaternion.identity);
                }
                if (burstNumber < numberOfProjectilesPerBurst - 1)
                {
                    yield return new WaitForSeconds(0.5f); //Time in between each burst fire
                }
            }
        } else
        {
            for (int projectileTypeInList = 0; projectileTypeInList < projectileList.Length; projectileTypeInList++)
            {
                for (int burstNumber = 0; burstNumber < burstList[projectileTypeInList]; burstNumber++)
                {
                    for (bulletParentNumber = 0; bulletParentNumber < bulletParents.Length; bulletParentNumber++)
                    {
                        Instantiate(projectiles[projectileList[projectileTypeInList]], bulletParents[bulletParentNumber].transform.position, Quaternion.identity);
                    }
                    if (burstNumber < burstList[projectileTypeInList] - 1)
                    {
                        yield return new WaitForSeconds(0.5f); //Time in between each burst fire
                    }
                }
            }
        }
    }
}
