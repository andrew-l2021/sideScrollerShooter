using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMiddle : BossOne
{
    // Update is called once per frame
    void Update()
    {
        if (bossMasterClass.currentHealth <= 70)
        {
            bossMasterClass.changePhase();
            DisplayCombo.instance.AddPoints(points);
            Destroy(gameObject);
        }

        //Phase Changes
        if (bossMasterClass.phase == 1) //Spawns regular enemies in sine wave at a burst fire of 2 per burst
        {
            //Boss Projectile Settings
            bossProjectileSpawner.randomFire = false;
            bossProjectileSpawner.moreThanOneProjectile = false;
            bossProjectileSpawner.numberOfProjectilesPerBurst = 2;
            bossProjectileSpawner.fireRate = 5;
            bossProjectileSpawner.projectileNumber = 0;
        }
        if (bossMasterClass.phase == 2) //Spawns EnemyShooterBasics at random intervals
        {
            //Boss Projectile Settings
            bossProjectileSpawner.randomFire = true;
            bossProjectileSpawner.moreThanOneProjectile = false;
            bossProjectileSpawner.numberOfProjectilesPerBurst = 1;
            bossProjectileSpawner.randomFireMinimumTime = 4;
            bossProjectileSpawner.randomFireMaximumTime = 8;
            bossProjectileSpawner.projectileNumber = 1;
        }
    }
}
