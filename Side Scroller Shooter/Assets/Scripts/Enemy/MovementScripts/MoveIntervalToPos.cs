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
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        active = startActive;
        targetLocation = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        pos = transform.position;

        if(active && timer > UpTimeInterval){ //switch from active to inactive
            active = false;
            timer = 0;
        }
        else if(active){ //movement while active
            transform.position = Vector2.MoveTowards(pos, targetLocation, moveSpeed * Time.fixedDeltaTime);
        }
        else if(!active && timer > DownTimeInterval){ //switch to from inactive to active
            targetLocation = GameObject.Find("Player").transform.position;
            active = true;
            timer = 0;
        }
        
    }
}