using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] protected float moveSpeed = 5;

    //Instance Variables
    protected Vector2 pos;
    protected float freezeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addFreezeTime(float addFreezeTime)
    {
        freezeTime += addFreezeTime;
    }
}
