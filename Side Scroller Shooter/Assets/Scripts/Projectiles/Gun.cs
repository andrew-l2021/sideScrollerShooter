using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Inspector Variables
    public Bullet bullet;

    //Instance Variables
    Vector2 direction;
    private Player playerGameObject;
    private float damagePercentage;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    public void Shoot()
    {
        damagePercentage = playerGameObject.currentDamagePercentage; //Get damagePercentage from Player Object

        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity); //Create Bullet Object
        Bullet goBullet = go.GetComponent<Bullet>(); //Get Component of Bullet to modify values
        //Debug.Log("Bullet Damage: " + goBullet.bulletDamage * damagePercentage); //Debug to check bullet damage with damagePercentage modifier
        goBullet.bulletDamage *= damagePercentage; //Modify bulletDamage by damagePercentage
        goBullet.direction = direction; //set direction of bullet to the direction that the gun is pointing
    }
}
