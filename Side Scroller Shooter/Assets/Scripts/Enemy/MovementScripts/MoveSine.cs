using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSine : MovementBase
{
    //Inspector Variables
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] bool inverted;

    //Instance Variables
    float sinCenterY;

    // Start is called before the first frame update
    void Start()
    {
        sinCenterY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(freezeTime > 0){
            freezeTime -= Time.fixedDeltaTime;
        }else{
            pos = transform.position;

            //y position calculation
            float sin = Mathf.Sin(pos.x * frequency) * amplitude;
            if (inverted)
            {
                sin *= -1;
            }
            pos.y = sinCenterY + sin;

            //x position calculation
            pos.x -= moveSpeed * Time.deltaTime;

            //update position
            transform.position = pos;
        }
        
    }
}
