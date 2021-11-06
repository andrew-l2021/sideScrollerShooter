using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBlaster : Blaster
{
    //Inspector variables
    //Single Q Variables
    [Header("Single Q Parameters")]
    [SerializeField] private Bullet bigSQBulletObject;
    [SerializeField] private Bullet smallSQBulletObject;

    //Double Q Variables
    [Header("Double Q Parameters")]
    [SerializeField] private ExplosiveBall ExplosiveDQObject;

    //Triple Q Variables
    [Header("Triple Q Parameters")]
    [SerializeField] private ExplosiveHomingBall ExplosiveTQObject;
    [SerializeField] private float numberOfExplosives = 8;
    [SerializeField] private float explosivesSpawnRadius = 1;
    [SerializeField] private float timeBetweenExplosivesSpawn = 0.1F;
    

    //Note: SQ stands for Single Q, DQ stands for Double Q, TQ stands for Triple Q
    //To Do: Make Bullets phase through/destroy enemy bullets

    public override void singleCombo()
    {
        Debug.Log("QBlaster_SingleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        GameObject BB = Instantiate(bigSQBulletObject.gameObject, transform.position, Quaternion.identity);
            Bullet bigBullet = BB.GetComponent<Bullet>();
            bigBullet.bulletDamage *= damagePercentage;

        GameObject SBU1 = Instantiate(smallSQBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * 6));
            Bullet smallBulletUp1 = SBU1.GetComponent<Bullet>();
            smallBulletUp1.bulletDamage *= damagePercentage;

        GameObject SBU2 = Instantiate(smallSQBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * 12));
            Bullet smallBulletUp2 = SBU2.GetComponent<Bullet>();
            smallBulletUp2.bulletDamage *= damagePercentage;

        GameObject SBD1 = Instantiate(smallSQBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * -6));
            Bullet smallBulletDown1 = SBD1.GetComponent<Bullet>();
            smallBulletDown1.bulletDamage *= damagePercentage;

        GameObject SBD2 = Instantiate(smallSQBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * -12));
            Bullet smallBulletDown2 = SBD2.GetComponent<Bullet>();
            smallBulletDown2.bulletDamage *= damagePercentage;
        
    }

    public override void doubleCombo()
    {
        Debug.Log("QBlaster_DoubleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        GameObject ExpBall = Instantiate(ExplosiveDQObject.gameObject, transform.position, Quaternion.identity);
        ExplosiveBall explosiveBall = ExpBall.GetComponent<ExplosiveBall>();
        explosiveBall.damagePercentage = damagePercentage;
    }

    public override void tripleCombo()
    {
        Debug.Log("QBlaster_TripleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        StartCoroutine(tripleComboExecution(damagePercentage));
    }

    IEnumerator tripleComboExecution(float damagePercentage)
    {
        for(int i = 0; i < numberOfExplosives; i++){
            Debug.Log("Loop Running");
            Vector2 explosivePos = new Vector2(explosivesSpawnRadius * Mathf.Cos(2*Mathf.PI*(i/numberOfExplosives)), explosivesSpawnRadius * Mathf.Sin(2*Mathf.PI*(i/numberOfExplosives)));
            GameObject ExpHomingBall = Instantiate(ExplosiveTQObject.gameObject, (Vector2)transform.position + explosivePos, Quaternion.identity);
            ExplosiveHomingBall explosiveHomingBall = ExpHomingBall.GetComponent<ExplosiveHomingBall>();
            explosiveHomingBall.damagePercentage = damagePercentage;
            yield return new WaitForSeconds(timeBetweenExplosivesSpawn);
        }
    }
}
