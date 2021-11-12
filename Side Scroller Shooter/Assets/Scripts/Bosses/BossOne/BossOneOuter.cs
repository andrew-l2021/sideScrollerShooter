using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneOuter : BossOne
{
    // Update is called once per frame
    void Update()
    {
        if (bossMasterClass.currentHealth <= 80)
        {
            bossMasterClass.changePhase();
            DisplayCombo.instance.AddPoints(points);
            Destroy(gameObject);
        }
    }
}
