using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBlaster : Blaster
{
    //Inspector Variables
    [Header("Single E Parameters")]
    [SerializeField] private Bullet SEBulletObject;
    [SerializeField] private float maxSqrDistanceFromPlayer = 9;
    [SerializeField] private float showRangeTime = 2;

    [Header("Double E Parameters")]
    [SerializeField] private Bullet DEBulletObject;
    [SerializeField] private float distanceBetweenLightning = 3;
    [SerializeField] private float timeBetweenLightning = 0.5F;
    [SerializeField] private float timeBetweenLightningBullets = 0.5F;
    [SerializeField] private int numberOfBulletsPerStrike = 3;

    [Header("Triple Q Parameters")]
    [SerializeField] private Bullet TEBulletObject;
    [SerializeField] private ChargeSequence chargeObj;
    [SerializeField] private float chargeTime = 0.5F;
    [SerializeField] private int numberOfTWPulses = 3;
    [SerializeField] private float timeBetweenPulses = 0.5F;
    [SerializeField] private int numberOfBulletsPerPulse = 3;
    [SerializeField] private float timeBetweenPulseBullets = 0.05F;

    //Instance Variables
    //Single E Variables
    private GameObject[] allEnemies;
    private LineRenderer lineRenderer;
    private Vector3[] points = new Vector3[360];

    //Triple E Variables


    void Start(){
        playerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }

    public override void singleCombo()
    {
        Debug.Log("EBlaster_SingleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        StartCoroutine(showRange());

        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 pos = transform.position;
        foreach(GameObject potentialTarget in allEnemies){
            Vector2 potentialTargetPos = potentialTarget.transform.position;
            Vector2 distanceToTarget = potentialTargetPos - pos;
            if(distanceToTarget.sqrMagnitude < maxSqrDistanceFromPlayer){
                float rotationAngle = Mathf.Atan2(distanceToTarget.y, distanceToTarget.x) * Mathf.Rad2Deg;
                GameObject SEBulletGO = Instantiate(SEBulletObject.gameObject, transform.position, Quaternion.AngleAxis(rotationAngle, Vector3.forward));
                Bullet SEBullet = SEBulletGO.GetComponent<Bullet>();
                SEBullet.bulletDamage *= damagePercentage;
            }
        }
    }

    public override void doubleCombo()
    {
        Debug.Log("EBlaster_DoubleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;
        
        StartCoroutine(doubleComboExecution(damagePercentage));
    }

    public override void tripleCombo()
    {
        Debug.Log("EBlaster_TripleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        damagePercentage = playerGameObject.currentDamagePercentage;

        StartCoroutine(tripleComboExecution(damagePercentage));
    }

    void DrawCircle(int steps, float radius)
    {
        lineRenderer.positionCount = steps;
 
        for(int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep/(steps-1);
 
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);
 
            float x = radius * xScaled + transform.position.x;
            float y = radius * yScaled + transform.position.y;
            float z = -1;
 
            Vector3 currentPosition = new Vector3(x,y,z);
 
            lineRenderer.SetPosition(currentStep,currentPosition);
        }
    }

    IEnumerator showRange()
    {
        DrawCircle(360, Mathf.Sqrt(maxSqrDistanceFromPlayer - 3.5F));
        yield return new WaitForSeconds(showRangeTime);
        lineRenderer.positionCount = 0;
    }
    
//Quaternion.Euler(Vector3.forward * (maxAngleMagnitude - (2*maxAngleMagnitude*i)/(numberOfSmallBullets - 1)))

    IEnumerator doubleComboExecution(float damagePercentage)
    {
        for(float i = transform.position.x + distanceBetweenLightning; i < 10; i += distanceBetweenLightning)
        {
            for(int j = 0; j < numberOfBulletsPerStrike; j++){
                Vector2 bulletPos = new Vector2(i, 6);
                GameObject DEBulletGO = Instantiate(DEBulletObject.gameObject, bulletPos, Quaternion.Euler(Vector3.forward * -90));
                Bullet DEBullet = DEBulletGO.GetComponent<Bullet>();
                DEBullet.bulletDamage *= damagePercentage;
                yield return new WaitForSeconds(timeBetweenLightningBullets);
            }
            yield return new WaitForSeconds(timeBetweenLightning);
        }
    }

//yield return new WaitForSeconds(timeBetweenPulseBullets);

    IEnumerator tripleComboExecution(float damagePercentage)
    {
        ChargeSequence charge = Instantiate(chargeObj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(chargeTime);
        charge.ChargeDone();
        for(int i = 0; i < numberOfTWPulses; i++){
            for(int j = 0; j < numberOfBulletsPerPulse; j++)
            {
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            Vector2 pos = transform.position;
                foreach(GameObject target in allEnemies)
                {
                    Vector2 potentialTargetPos = target.transform.position;
                    Vector2 distanceToTarget = potentialTargetPos - pos;
                        if(target != null)
                        {
                            float rotationAngle = Mathf.Atan2(distanceToTarget.y, distanceToTarget.x) * Mathf.Rad2Deg;
                            GameObject TEBulletGO = Instantiate(TEBulletObject.gameObject, transform.position, Quaternion.AngleAxis(rotationAngle, Vector3.forward));
                            Bullet TEBullet = TEBulletGO.GetComponent<Bullet>();
                            TEBullet.bulletDamage *= damagePercentage;
                        }
                }
                yield return new WaitForSeconds(timeBetweenPulseBullets);
            }
            yield return new WaitForSeconds(timeBetweenPulses);
        }
    }
}
