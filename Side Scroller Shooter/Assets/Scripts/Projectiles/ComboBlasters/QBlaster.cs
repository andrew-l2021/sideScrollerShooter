using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBlaster : Blaster
{
    public override void singleCombo()
    {
        Debug.Log("QBlaster_SingleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }

    public override void doubleCombo()
    {
        Debug.Log("QBlaster_DoubleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }

    public override void tripleCombo()
    {
        Debug.Log("QBlaster_TripleComboFire | Current Damage Percentage: " + playerGameObject.currentDamagePercentage + " | element: " + element);
        //throw new System.NotImplementedException();
    }
}
