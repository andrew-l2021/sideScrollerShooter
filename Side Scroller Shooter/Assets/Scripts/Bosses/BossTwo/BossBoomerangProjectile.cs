using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoomerangProjectile : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] private GameObject player;
    [SerializeField] protected float damage;

    //Instance Variables
    private Vector2 targetLocation;
    private bool alreadyDamaged;
    private Vector2 swordPosition;

    private bool go; //Changes the direction of the boomerang bullet

    // Start is called before the first frame update
    void Start()
    {
        swordPosition = transform.position;
        go = true;
        alreadyDamaged = false;
        targetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        StartCoroutine(throwBoomerang());
    }

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 2000);

        //Boomerang Movement
        if (go) //Moves to player
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(-7 , targetLocation.y), Time.deltaTime * 4);
        }
        if (!go) //Moves out of screen
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(12, targetLocation.y), Time.deltaTime * 4);
        }
    }

    IEnumerator throwBoomerang()
    {
        yield return new WaitForSeconds(1.5f);
        targetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        go = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision) //Has a custom enemy detection script because it should finish animation even if hitting the player
    {
        if (collision.tag == "Player")
        {
            if (!alreadyDamaged)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                player.GetComponent<Player>().timeLastQBarChange = player.GetComponent<Player>().timer;
                player.GetComponent<Player>().timeLastWBarChange = player.GetComponent<Player>().timer;
                player.GetComponent<Player>().timeLastEBarChange = player.GetComponent<Player>().timer;
                player.GetComponent<Player>().currentQRate = player.GetComponent<Player>().maxQRate / 2; //Resets regeneration acceleration
                player.GetComponent<Player>().currentWRate = player.GetComponent<Player>().maxWRate / 2;
                player.GetComponent<Player>().currentERate = player.GetComponent<Player>().maxERate / 2;
                alreadyDamaged = true;
            }
        }
        if (collision.tag == "Shield")
        {
            if (!alreadyDamaged)
            {
                collision.GetComponent<Shield>().DestroyShield();
                alreadyDamaged = true;
            }
        }
    }
}
