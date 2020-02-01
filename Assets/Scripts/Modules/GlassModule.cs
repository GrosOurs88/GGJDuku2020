using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassModule : Module
{
    public float oxygenLoss50 = 0.3f;
    public float oxygenLoss25 = 1f;
    
    protected override void UpdateFullLife()
    {
        // Do nothing
    }

    protected override void UpdateDamaged()
    {
        shipManager.OxygenAmount -= oxygenLoss50 * Time.deltaTime;
    }

    protected override void UpdateDead()
    {
        shipManager.OxygenAmount -= oxygenLoss25 * Time.deltaTime;
    }
}
