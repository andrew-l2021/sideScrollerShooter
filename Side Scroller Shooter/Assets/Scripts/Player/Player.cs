using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Inspector variables (speed and fireRate should never be modified, only called)
    [SerializeField] float speed = 3;
    [SerializeField] float fireRate;
    

    //Instance variables
    float timer = 0;
    Gun[] guns;

    //Movement variables
    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;

    //Shooting variables
    bool shoot;
    bool shootUp; //shootUp differentiates between holding R and spamming R
    float lastShot = 0;

    //Property variables
    float currentSpeed;
    float currentFireRate;

    //Buff Timer variables
    float speedTime = 0;
    float fireRateTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
        currentSpeed = speed;
        currentFireRate = fireRate;
        //gunShoot = gunFireRate.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow);
        moveDown = Input.GetKey(KeyCode.DownArrow);
        moveLeft = Input.GetKey(KeyCode.LeftArrow);
        moveRight = Input.GetKey(KeyCode.RightArrow);

        timer += Time.deltaTime;

        //counts time for speed up buff
        if (speedTime > 0)
        {
            Debug.Log("Speeding!");
            speedTime -= Time.deltaTime;

            if(speedTime <= 0 )
            {
                //when the the timer is up end the speedboost
                currentSpeed = speed;
            }
        }

        //counts time for fire rate buff
        if (fireRateTime > 0)
        {
            Debug.Log("Fire Rate Increased!");
            fireRateTime -= Time.deltaTime;

            if (fireRateTime <= 0)
            {
                //when the the timer is up end the speedboost
                currentFireRate = fireRate;
            }
        }

        //Shooting
        shoot = Input.GetKey(KeyCode.R);
        if (shoot) //Spam shooting or Hold "R" shooting (limited to fireRate)
        {
            if (timer > currentFireRate + lastShot)
            {
                foreach (Gun gun in guns)
                {
                    gun.Shoot();
                }
                lastShot = timer;
            }
        }else{
            lastShot = timer - currentFireRate;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = currentSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        //Basic movement
        if (moveUp)
        {
            move.y += moveAmount;
        }
        if (moveDown)
        {
            move.y -= moveAmount;
        }
        if (moveLeft)
        {
            move.x -= moveAmount;
        }
        if (moveRight)
        {
            move.x += moveAmount;
        }

        //Adjust for faster diagonal movement
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        //Boundaries
        pos += move;
        if (pos.x <= -8.32f)
        {
            pos.x = -8.32f;
        }
        if (pos.x >= 8.32f)
        {
            pos.x = 8.32f;
        }
        if (pos.y >= 4.47f)
        {
            pos.y = 4.47f;
        }
        if (pos.y <= -4.47f)
        {
            pos.y = -4.47f;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            Destroy(gameObject);
            Destroy(bullet);
        }
    }

    public void TemporarilyIncreaseFireRate(float fireRateModifier, int time)
    {
        //using subtract because rate of fire increases as currentFireRate decreases
        currentFireRate -= fireRateModifier;
        fireRateTime = time;
    }

    public void TemporarilyIncreaseSpeed(float speedModifier, int time)
    {
        //using addition because speed increases as currentSpeed increases
        currentSpeed += speedModifier;
        speedTime = time;
    }

    
}
