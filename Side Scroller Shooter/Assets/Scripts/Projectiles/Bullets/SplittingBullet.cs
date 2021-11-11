using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingBullet : Bullet
{
    //Inspector Variables
    [SerializeField] private float initialSpeed = 5;
    //[SerializeField] private float acceleration = 0;
    //[SerializeField] private float jerk = 1.0F; //derivative of acceleration
    [SerializeField] private int numberOfSplittingBullets = 5;
    [SerializeField] private float timeBetweenSplitBulletSpawn = 0.25F;
    [SerializeField] private float freezeTime = 3;

    //Instance Variables
    private Vector2 pos;
    private float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        speed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        pos = transform.position;
        pos += (Vector2)transform.right * speed * Time.fixedDeltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            MovementBase enemyMovement = collision.GetComponent<MovementBase>();
            enemyMovement.addFreezeTime(freezeTime);
            StartCoroutine(splitBullets());
        }
    }

    IEnumerator splitBullets()
    {
        yield return new WaitForSeconds(timeBetweenSplitBulletSpawn);
        for(int i = 0; i < numberOfSplittingBullets; i++){
            float degreeRotation = ((i * (360/numberOfSplittingBullets)) + 90);
            Debug.Log("Spawn #: " + i + " | degreeRotation: " + degreeRotation);
            GameObject splitBulletObj = Instantiate(gameObject, transform.position, Quaternion.Euler(Vector2.right * degreeRotation));
            SplittingBullet splitBullet = splitBulletObj.GetComponent<SplittingBullet>();
            splitBullet.bulletDamage = this.bulletDamage;
            yield return new WaitForSeconds(timeBetweenSplitBulletSpawn);
        }
    }
}
