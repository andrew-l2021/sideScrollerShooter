using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Inspector variables (these variables should never be modified, only called)
    [SerializeField] float speed = 3;
    [SerializeField] float fireRate;
    [SerializeField] float damagePercentage = 1.00F;
    

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
    float currentDamagePercentage;

    //Buff Timer variables
    float speedBuffTime = 0;
    float fireRateBuffTime = 0;
    float damageBuffTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
        currentSpeed = speed;
        currentFireRate = fireRate;
        currentDamagePercentage = damagePercentage;
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
        if (speedBuffTime > 0)
        {
            speedBuffTime -= Time.deltaTime;

            if(speedBuffTime <= 0 )
            {
                //when the the timer is up end the speedboost
                currentSpeed = speed;
            }
        }

        //counts time for fire rate buff
        if (fireRateBuffTime > 0)
        {
            fireRateBuffTime -= Time.deltaTime;

            if (fireRateBuffTime <= 0)
            {
                //when the the timer is up end the speedboost
                currentFireRate = fireRate;
            }
        }

        //counts time for damage buff
        if (damageBuffTime > 0)
        {
            damageBuffTime -= Time.deltaTime;

            if (damageBuffTime <= 0)
            {
                //when the the timer is up end the speedboost
                currentDamagePercentage = damagePercentage;
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
                    gun.Shoot(currentDamagePercentage);
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
        Debug.Log("Fire Rate Increased!");
        currentFireRate -= fireRateModifier;
        fireRateBuffTime = time;
    }

    public void TemporarilyIncreaseSpeed(float speedModifier, int time)
    {
        //using addition because speed increases as currentSpeed increases
        Debug.Log("Speeding!");
        currentSpeed += speedModifier;
        speedBuffTime = time;
    }

    public void TemporarilyIncreaseDamage(float damageModifier, int time)
    {
        //1 = 100% damage, 1 * (1 + 0.20) = 1.20 = 120%
        Debug.Log("Damage Increased!");
        currentDamagePercentage *= (1 + damageModifier);
        damageBuffTime = time;
    }
}
