using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBlaster : Blaster
{
    //Inspector Variables
    [Header("Single W Parameters")]
    [SerializeField] private Bullet SWBulletObject;
    [SerializeField] private int numberOfSWBulletsX4 = 8;
    [SerializeField] private float maxAngleMagnitude = 60;
    [SerializeField] private float timeBetweenSWSpawn = 0.1F;

    [Header("Double W Parameters")]
    [SerializeField] private CircularMotionBullet DWBulletObject;
    [SerializeField] private int numberOfDWBulletsX3 = 8;
    [SerializeField] private float timeBetweenDWBulletsSpawn = 0.1F;

    [Header("Triple W Parameters")]
    [SerializeField] private ExplosiveFreezeBall ExplosiveTWObject;
    
    public override void singleCombo()
    {
        Debug.Log("WBlaster_SingleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        StartCoroutine(singleComboExecution(damagePercentage));
        //throw new System.NotImplementedException();
    }

    public override void doubleCombo()
    {
        Debug.Log("WBlaster_DoubleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        StartCoroutine(doubleComboExecution(damagePercentage));
    }

    public override void tripleCombo()
    {
        Debug.Log("WBlaster_TripleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        GameObject ExpFreezeBall = Instantiate(ExplosiveTWObject.gameObject, transform.position, Quaternion.identity);
        ExplosiveFreezeBall explosiveFreezeBall = ExpFreezeBall.GetComponent<ExplosiveFreezeBall>();
        explosiveFreezeBall.damagePercentage = damagePercentage;
    }

    IEnumerator singleComboExecution(float damagePercentage){
        for (int i = 0; i < numberOfSWBulletsX4; i++){
            GameObject smallBulletGO = Instantiate(SWBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * (maxAngleMagnitude - (2*maxAngleMagnitude*i)/(numberOfSWBulletsX4 - 1))));
            GameObject smallOppBulletGO = Instantiate(SWBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * -(maxAngleMagnitude - (2*maxAngleMagnitude*i)/(numberOfSWBulletsX4 - 1))));
            Bullet smallBullet = smallBulletGO.GetComponent<Bullet>();
            Bullet smallOppBullet = smallOppBulletGO.GetComponent<Bullet>();
            smallBullet.bulletDamage *= damagePercentage;
            smallOppBullet.bulletDamage *= damagePercentage;
            yield return new WaitForSeconds(timeBetweenSWSpawn);
        }
        for (int i = 0; i < numberOfSWBulletsX4; i++){
            GameObject smallBulletGO = Instantiate(SWBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * (maxAngleMagnitude - (2*maxAngleMagnitude*i)/(numberOfSWBulletsX4 - 1))));
            GameObject smallOppBulletGO = Instantiate(SWBulletObject.gameObject, transform.position, Quaternion.Euler(Vector3.forward * -(maxAngleMagnitude - (2*maxAngleMagnitude*i)/(numberOfSWBulletsX4 - 1))));
            Bullet smallBullet = smallBulletGO.GetComponent<Bullet>();
            Bullet smallOppBullet = smallOppBulletGO.GetComponent<Bullet>();
            smallBullet.bulletDamage *= damagePercentage;
            smallOppBullet.bulletDamage *= damagePercentage;
            yield return new WaitForSeconds(timeBetweenSWSpawn);
        }
    }

    IEnumerator doubleComboExecution(float damagePercentage)
    {
        for(int i = 0; i < numberOfDWBulletsX3; i++){
            //Debug.Log("Loop Running");
            float degreeRotation1 = (i * (360/numberOfDWBulletsX3));
            //Debug.Log("Degree Rotation 1: " + degreeRotation1);
            GameObject DWB1 = Instantiate(DWBulletObject.gameObject, transform.position, Quaternion.identity);
            CircularMotionBullet DWBullet1 = DWB1.GetComponent<CircularMotionBullet>();
            DWBullet1.bulletDamage *= damagePercentage;
            DWBullet1.setAngle(degreeRotation1);

            float degreeRotation2 = (i * (360/numberOfDWBulletsX3) + (120));
            //Debug.Log("Degree Rotation 2: " + degreeRotation2);
            GameObject DWB2 = Instantiate(DWBulletObject.gameObject, transform.position, Quaternion.identity);
            CircularMotionBullet DWBullet2 = DWB2.GetComponent<CircularMotionBullet>();
            DWBullet2.bulletDamage *= damagePercentage;
            DWBullet2.setAngle(degreeRotation2);

            float degreeRotation3 = (i * (360/numberOfDWBulletsX3) + (240));
            //Debug.Log("Degree Rotation 3: " + degreeRotation3);
            GameObject DWB3 = Instantiate(DWBulletObject.gameObject, transform.position, Quaternion.identity);
            CircularMotionBullet DWBullet3 = DWB3.GetComponent<CircularMotionBullet>();
            DWBullet3.bulletDamage *= damagePercentage;
            DWBullet3.setAngle(degreeRotation3);

            yield return new WaitForSeconds(timeBetweenDWBulletsSpawn);
        }
    }

}
