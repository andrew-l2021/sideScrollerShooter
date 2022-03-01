using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwo : MonoBehaviour
{
    //Inspector Variables
    [Header("Stats")]
    [SerializeField] private float startingHealth;
    [SerializeField] private BossProjectileSpawner bossProjectileSpawner;
    [SerializeField] private GameObject player;
    [Header("Sword Attributes")]
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordParent;
    [Header("Gun Attributes")]
    [SerializeField] private GameObject gunParentOne;
    [SerializeField] private GameObject gunParentTwo;
    [Header("Laser Attributes")]
    [SerializeField] private LineRenderer laserOneLineRenderer;
    [SerializeField] private LineRenderer laserTwoLineRenderer;
    [SerializeField] private float laserWidth = 0.1f;

    //Instance Variables
    private int[] xGrid = { 2, 7 }; //Determines the allowable grid coordinates that the boss can traverse to
    private int[] yGrid = { -3, 3 };
    private Vector2 randomGridPoint;
    private float enemyMoveStart;
    private float moveSet;
    private float randomY;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = 20;
        bossProjectileSpawner.activelyFiring = false;
        laserOneLineRenderer.enabled = false;
        laserTwoLineRenderer.enabled = false;
        laserOneLineRenderer.startWidth = laserWidth;
        laserOneLineRenderer.endWidth = laserWidth;
        laserTwoLineRenderer.startWidth = laserWidth;
        laserTwoLineRenderer.endWidth = laserWidth;

        StartCoroutine(executeMoveset());
    }

    // Update is called once per frame
    void Update()
    {
        if (startingHealth <= 0)
        {
            DisplayCombo.instance.AddPoints(100);
            Destroy(gameObject);
        }
    }

    IEnumerator executeMoveset()
    {
        while (startingHealth > 0)
        {
            //Move 2-3 times inside of the allowable grid
            for (int i = 0; i < Random.Range(2, 4); i++)
            {
                randomGridPoint = new Vector2(Random.Range(xGrid[0], xGrid[1]), Random.Range(yGrid[0], yGrid[1]));
                enemyMoveStart = Time.time;
                while (Time.time < enemyMoveStart + 0.8f)
                {
                    transform.position = Vector2.Lerp(transform.position, randomGridPoint, Time.deltaTime * 4);
                    yield return null;
                }
            }

            //Perform certain moves
            moveSet = Random.Range(1, 4);

            if (moveSet == 1)
            {
                StartCoroutine(throwSword());
                yield return new WaitForSeconds(4.5f);
            } else
            {
                if (moveSet == 2)
                {
                    StartCoroutine(doubleLasers());
                    yield return new WaitForSeconds(7);
                } else
                {
                    StartCoroutine(bulletSpray());
                    yield return new WaitForSeconds(6);
                }
            }
        }
    }


    IEnumerator throwSword()
    {
        enemyMoveStart = Time.time;
        randomY = Random.Range(-3, 4);
        while (Time.time < enemyMoveStart + 1)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(7.25f, randomY), Time.deltaTime * 4);
            yield return null;
        }
        Debug.Log(transform.position + " " + Random.state);
        yield return new WaitForSeconds(0.5f); //ANIMATE WARNING PERIOD HERE
        Instantiate(sword, swordParent.transform.position, Quaternion.identity);
        enemyMoveStart = Time.time;
        randomY = Random.Range(-3, 4);
        while (Time.time < enemyMoveStart + 1)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(7.25f, randomY), Time.deltaTime * 4);
            yield return null;
        }
        Debug.Log(transform.position + " " + Random.state);
        yield return new WaitForSeconds(0.5f); //ANIMATE WARNING PERIOD HERE
        Instantiate(sword, swordParent.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3);
    }

    IEnumerator doubleLasers()
    {
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 1)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(7.25f, 3), Time.deltaTime * 4);
            yield return null;
        }
        yield return new WaitForSeconds(1); //ANIMATE WARNING PERIOD HERE
        StartCoroutine(shootLasers());
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(7.25f, -3), Time.deltaTime * 2f);
            yield return null;
        }
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 1)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(7.25f, 0), Time.deltaTime * 4);
            yield return null;
        }
    }

    IEnumerator shootLasers() //Shoots lasers that are activated at the beginning and end and are turned off in the middle
    {
        bool gunOneAlreadyHit = false;
        bool gunTwoAlreadyHit = false;
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 1.25f)
        {
            RaycastHit2D gunOneHit = Physics2D.Raycast(gunParentOne.transform.position, Vector2.left, Mathf.Infinity, 1 << 6); //Creates a raycast that only triggers if hitting an object in layer 6 (player layer)
            RaycastHit2D gunTwoHit = Physics2D.Raycast(gunParentTwo.transform.position, Vector2.left, Mathf.Infinity, 1 << 6);
            laserOneLineRenderer.enabled = true;
            laserTwoLineRenderer.enabled = true;
            laserOneLineRenderer.SetPosition(0, gunParentOne.transform.position);
            laserOneLineRenderer.SetPosition(1, new Vector2(-20, gunParentOne.transform.position.y));
            laserTwoLineRenderer.SetPosition(0, gunParentTwo.transform.position);
            laserTwoLineRenderer.SetPosition(1, new Vector2(-20, gunParentTwo.transform.position.y));
            if (gunOneHit && !gunOneAlreadyHit)
            {
                player.GetComponent<Health>().TakeDamage(5);
                gunOneAlreadyHit = true;
            }
            if (gunTwoHit && !gunTwoAlreadyHit)
            {
                player.GetComponent<Health>().TakeDamage(5);
                gunTwoAlreadyHit = true;
            }
            yield return null;
        }

        laserOneLineRenderer.enabled = false;
        laserTwoLineRenderer.enabled = false;
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 0.5f)
        {
            yield return null;
        }

        gunOneAlreadyHit = false;
        gunTwoAlreadyHit = false;
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 1.25f)
        {
            RaycastHit2D gunOneHit = Physics2D.Raycast(gunParentOne.transform.position, Vector2.left, Mathf.Infinity, 1 << 6); //Creates a raycast that only triggers if hitting an object in layer 6 (player layer)
            RaycastHit2D gunTwoHit = Physics2D.Raycast(gunParentTwo.transform.position, Vector2.left, Mathf.Infinity, 1 << 6);
            laserOneLineRenderer.enabled = true;
            laserTwoLineRenderer.enabled = true;
            laserOneLineRenderer.SetPosition(0, gunParentOne.transform.position);
            laserOneLineRenderer.SetPosition(1, new Vector2(-20, gunParentOne.transform.position.y));
            laserTwoLineRenderer.SetPosition(0, gunParentTwo.transform.position);
            laserTwoLineRenderer.SetPosition(1, new Vector2(-20, gunParentTwo.transform.position.y));
            if (gunOneHit && !gunOneAlreadyHit)
            {
                player.GetComponent<Health>().TakeDamage(5);
                gunOneAlreadyHit = true;
            }
            if (gunTwoHit && !gunTwoAlreadyHit)
            {
                player.GetComponent<Health>().TakeDamage(5);
                gunTwoAlreadyHit = true;
            }
            yield return null;
        }
        laserOneLineRenderer.enabled = false;
        laserTwoLineRenderer.enabled = false;
    }

    IEnumerator bulletSpray() //Initiates a sine wave movement and a regular bullet spray
    {
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 1)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(7.25f, 0), Time.deltaTime * 4);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f); //ANIMATE WARNING PERIOD HERE
        bossProjectileSpawner.activelyFiring = true;
        bossProjectileSpawner.randomFire = true; ;
        bossProjectileSpawner.moreThanOneProjectile = false;
        bossProjectileSpawner.numberOfProjectilesPerBurst = 1;
        bossProjectileSpawner.randomFireMinimumTime = 0.1f;
        bossProjectileSpawner.randomFireMaximumTime = 0.2f;
        bossProjectileSpawner.projectileNumber = 0;
        StartCoroutine(moveWave());
        yield return new WaitForSeconds(3);
        bossProjectileSpawner.activelyFiring = false;
        enemyMoveStart = Time.time;
        while (Time.time < enemyMoveStart + 1.5f)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(7.25f, 0), Time.deltaTime * 4);
            yield return null;
        }
    }

    IEnumerator moveWave() //Calculates sine wave for the boss to move in
    {
        enemyMoveStart = Time.time;
        float sinCenterY = 0f;
        float sinCenterX = transform.position.x;

        while (Time.time < enemyMoveStart + 3) //script runs for 3 seconds
        {
            Vector2 pos = transform.position;

            float sin = Mathf.Sin((sinCenterX - pos.x) * 3) * 3; //y position calculation
            pos.y = sinCenterY + sin;
            pos.x += (enemyMoveStart - Time.time) * 0.005f; //x position calculation

            //update position
            transform.position = pos;
            yield return null;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            //ignore destroy code if Enemy cannot be destroyed
            if (transform.position.x > 10)
            {
                return;
            }

            //subtract bulletDamage from the currentHealth of the Enemy
            Bullet bullet = collision.GetComponent<Bullet>();
            float damageToBeDone = bullet.bulletDamage;
            Destroy(bullet.gameObject);
            startingHealth -= damageToBeDone;
        }
        if (collision.tag == "Explosion")
        {
            Explosive explosive = collision.GetComponent<Explosive>();
            startingHealth -= explosive.explosionDamage;
        }
    }
}
