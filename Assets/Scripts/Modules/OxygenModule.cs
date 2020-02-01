using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenModule : Module
{

    public float oxygenRefill100 = 1;
    public float oxygenRefill50 = 0.5f;
    public float oxygenRefill25 = 0;
    
    
    protected override void UpdateFullLife()
    {
        shipManager.OxygenAmount += oxygenRefill100 * Time.deltaTime;
    }

    protected override void UpdateDamaged()
    {
        shipManager.OxygenAmount += oxygenRefill50 * Time.deltaTime;
    }

    protected override void UpdateDead()
    {
        shipManager.OxygenAmount += oxygenRefill25 * Time.deltaTime;
    }
}
