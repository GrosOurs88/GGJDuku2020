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
        shipManager.oxygenAmount += oxygenRefill100 * Time.deltaTime;
    }

    protected override void UpdateDamaged()
    {
        shipManager.oxygenAmount += oxygenRefill50 * Time.deltaTime;
    }

    protected override void UpdateDead()
    {
        shipManager.oxygenAmount += oxygenRefill25 * Time.deltaTime;
    }
}
