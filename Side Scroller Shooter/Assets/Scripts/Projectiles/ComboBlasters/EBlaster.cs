using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBlaster : Blaster
{
    public override void singleCombo()
    {
        Debug.Log("EBlaster_SingleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }

    public override void doubleCombo()
    {
        Debug.Log("EBlaster_DoubleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }

    public override void tripleCombo()
    {
        Debug.Log("EBlaster_TripleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }
}
