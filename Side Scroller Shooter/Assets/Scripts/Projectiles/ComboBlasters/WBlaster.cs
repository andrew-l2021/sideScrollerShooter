using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBlaster : Blaster
{
    public override void singleCombo()
    {
        Debug.Log("WBlaster_SingleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }

    public override void doubleCombo()
    {
        Debug.Log("WBlaster_DoubleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }

    public override void tripleCombo()
    {
        Debug.Log("WBlaster_TripleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }
}
