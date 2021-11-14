using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Blaster : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] public string element;

    //Instance Variables
    protected Player playerGameObject; //protected allows for all extending classes to call playerGameObject, acts as private in extended classes
    protected float damagePercentage;

    //Methods
    public abstract void singleCombo();
    public abstract void doubleCombo();
    public abstract void tripleCombo();

    void Start(){
        playerGameObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        //direction = (transform.localRotation * Vector2.right).normalized;
    }

}
