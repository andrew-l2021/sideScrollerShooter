using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneInner : BossOne
{
    //Inspector Variables
    public float rotationConstant;
    public AnimationCurve rotationCurve;

    //Instance Variables
    private float rotationRate;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        //Rotation
        timer += Time.deltaTime;
        if (Time.timeScale != 0) //Checks if game is not paused
        {
            rotationRate = rotationCurve.Evaluate(timer) * rotationConstant;
            transform.RotateAround(colliderBounds.bounds.center, Vector3.forward, rotationRate);
        }

        //Death
        if (bossMasterClass.currentHealth <= 0)
        {
            DisplayCombo.instance.AddPoints(points);
            bossMasterClass.destroyObject();
            Destroy(gameObject);
        }

        //Phase Changes
        if (bossMasterClass.phase == 1) //Fires regular bullets from a rotating gun (in spurts of 3 bullets), spins slowly
        {
            //Rotation Settings
            rotationConstant = 1;

            //Boss Projectile Settings
            bossProjectileSpawner.activelyFiring = true;
            bossProjectileSpawner.randomFire = false;
            bossProjectileSpawner.moreThanOneProjectile = false;
            bossProjectileSpawner.numberOfProjectilesPerBurst = 3;
            bossProjectileSpawner.fireRate = 3;
            bossProjectileSpawner.projectileNumber = 0;
        }
        if (bossMasterClass.phase == 2) //Rotating gun firing rate ramps up, fires regular bullets, regular firing rate, spins faster
        {
            //Rotation Settings
            rotationConstant = 3;

            //Boss Projectile Settings
            bossProjectileSpawner.activelyFiring = true;
            bossProjectileSpawner.randomFire = false;
            bossProjectileSpawner.moreThanOneProjectile = false;
            bossProjectileSpawner.numberOfProjectilesPerBurst = 1;
            bossProjectileSpawner.fireRate = 0.75f;
            bossProjectileSpawner.projectileNumber = 0;
        }
        if (bossMasterClass.phase == 3) //Fires regular bullets and splitter enemies with 5 linears inside, spins very rapidly
        {
            //Rotation Settings
            rotationConstant = 5;

            //Boss Projectile Settings
            bossProjectileSpawner.activelyFiring = true;
            bossProjectileSpawner.randomFire = false;
            bossProjectileSpawner.moreThanOneProjectile = true;
            bossProjectileSpawner.fireRate = 15;

            bossProjectileSpawner.projectileList = new int[3];
            bossProjectileSpawner.projectileList[0] = 0;
            bossProjectileSpawner.projectileList[1] = 1;
            bossProjectileSpawner.projectileList[2] = 0;


            bossProjectileSpawner.burstList = new int[3];
            bossProjectileSpawner.burstList[0] = 15;
            bossProjectileSpawner.burstList[1] = 1;
            bossProjectileSpawner.burstList[2] = 15;

        }
    }
}
