using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassModule : Module
{
    public float oxygenLoss50 = 0.3f;
    public float oxygenLoss25 = 1f;
    
    
    public float temperatureLoss50 = 0.3f;
    public float temperatureLoss25 = 1;
    
    protected override void UpdateFullLife()
    {
        // Do nothing
    }

    protected override void UpdateDamaged()
    {
        shipManager.OxygenAmount -= oxygenLoss50 * Time.deltaTime;
        shipManager.TemperatureAmount -= temperatureLoss50 * Time.deltaTime;
    }

    protected override void UpdateDead()
    {
        shipManager.OxygenAmount -= oxygenLoss25 * Time.deltaTime;
        shipManager.TemperatureAmount -= temperatureLoss25 * Time.deltaTime;
    }
}
