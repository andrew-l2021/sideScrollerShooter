using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSequence : MonoBehaviour
{

    private Player playerGameObject;

    public void ChargeDone()
    {
        Destroy(gameObject);
    }
    
    void Start(){
        playerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update(){
        transform.position = playerGameObject.transform.position;
    }
}
