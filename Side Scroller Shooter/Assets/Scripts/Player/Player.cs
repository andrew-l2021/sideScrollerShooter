using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 3;
    [SerializeField] float fireRate;
    float timer = 0;
    Gun[] guns;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;

    bool shoot;
    bool shootUp; //shootUp differentiates between holding R and spamming R
    float lastShot = 0;

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
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

        //Shooting
        shoot = Input.GetKey(KeyCode.R);
        if (shoot) //Spam shooting or Hold "R" shooting (limited to fireRate)
        {
            if (timer > fireRate + lastShot)
            {
                print("should be firing");
                foreach (Gun gun in guns)
                {
                    print("firing");
                    gun.Shoot();
                }
                lastShot = timer;
            }
        }else{
            lastShot = timer - fireRate;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = Speed * Time.fixedDeltaTime;
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

    public void IncreaseFireRate(float val)
    {
        fireRate += val;
    }
}
