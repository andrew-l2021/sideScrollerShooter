using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves towards Player position, will alternate between moving and inactive for set time intervals
public class MoveIntervalToPos : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float UpTimeInterval = 5;
    [SerializeField] float DownTimeInterval = 5;
    [SerializeField] bool startActive = true;
    bool active;
    float timer = 0;
    Vector2 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        active = startActive;
        targetLocation = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if(active && timer > UpTimeInterval){
            active = false;
            timer = 0;
        }else if(active){
            transform.position = Vector2.MoveTowards(pos, targetLocation, moveSpeed * Time.fixedDeltaTime);
        }else if(!active && timer > DownTimeInterval){
            targetLocation = GameObject.Find("Player").transform.position;
            active = true;
            timer = 0;
        }
        
    }
}