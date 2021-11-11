using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotionBullet : Bullet
{
    //Inspector variables
    [SerializeField] private float angularSpeed = 10F;
    [Header("Delay Parameters")]
    [SerializeField] private float radiusSpeedDelay = 5;
    [SerializeField] private float radiusAccelerationDelay = -10F;
    [SerializeField] private float radiusJerkDelay = 0F; //derivative of acceleration
    [Header("Active Parameters")]
    [SerializeField] private float radiusSpeedActive = 0;
    [SerializeField] private float radiusAccelerationActive = 0.5F;
    [SerializeField] private float radiusJerkActive = 1.0F; //derivative of acceleration
    [Header("Timer Parameters")]
    [SerializeField] private float delayTime = 0.5F;
    [SerializeField] private float idleTime = 0.5F;
    [SerializeField] private float lifetime = 4;
    

    //Instance variables
    private float timer = 0;
    private float circleRad;
    public float currentAngle { get; private set; } = 0;

    Player playerGameObject;
    //Vector2 posFromPlayer;
    Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPos = playerGameObject.transform.position;
        
    }

    public void setAngle(float degreeValue){
        currentAngle = degreeValue * (Mathf.PI/180);
        Debug.Log("Current Angle: " + currentAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > (lifetime + idleTime + delayTime))
        {
            Destroy(gameObject);
        }
    }

    /*private void FixedUpdate()
    {
        if(timer < delayTime){
            accelerationDelay += jerkDelay * Time.fixedDeltaTime;
            delaySpeed += accelerationDelay * Time.fixedDeltaTime;
            posFromPlayer = posFromPlayer + (Vector2)transform.right * delaySpeed * Time.fixedDeltaTime;
            transform.position = (Vector2)playerGameObject.transform.position + posFromPlayer;
        }else if(timer < (idleTime + delayTime)){
            transform.position = (Vector2)playerGameObject.transform.position + posFromPlayer;
            circleRad = posFromPlayer.magnitude;
            currentAngle = Vector2.Angle(transform.position, playerGameObject.transform.position);
            Debug.Log("CurrentAngle" + currentAngle);
        }else{
            //induce circular motion with contant velocity opposing centrifugal acceleration
            Vector2 offset = new Vector2 (Mathf.Sin (currentAngle), Mathf.Cos (currentAngle)) * circleRad;
            currentAngle += angularSpeed + Time.fixedDeltaTime;
            transform.position = (Vector2)playerGameObject.transform.position + offset;
            circleRad += radiusIncreaseVelocity * Time.fixedDeltaTime;
        }
    }*/
    
    private void FixedUpdate(){
        timer += Time.fixedDeltaTime;
        Vector2 offset = new Vector2 (Mathf.Sin (currentAngle), Mathf.Cos (currentAngle)) * circleRad;
        transform.position = (Vector2)playerGameObject.transform.position + offset;

        if(timer < delayTime){
            radiusAccelerationDelay += radiusJerkDelay * Time.fixedDeltaTime;
            radiusSpeedDelay += radiusAccelerationDelay * Time.fixedDeltaTime;
            circleRad += radiusSpeedDelay * Time.fixedDeltaTime;
        }else if(timer > (idleTime + delayTime)){
            radiusAccelerationActive += radiusJerkActive * Time.fixedDeltaTime;
            radiusSpeedActive += radiusAccelerationActive * Time.fixedDeltaTime;
            circleRad += radiusSpeedActive * Time.fixedDeltaTime;
        }
        currentAngle += angularSpeed * Time.fixedDeltaTime;
    }
}
