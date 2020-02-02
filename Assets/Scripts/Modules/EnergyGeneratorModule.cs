using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorModule : Module
{
    public float energy100 = 5;
    public float energy50 = 2;
    public float energy25 = 0;
    
    protected override void UpdateFullLife()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shipManager.EnergyAmount += energy100;
        }
    }

    protected override void UpdateDamaged()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shipManager.EnergyAmount += energy50;
        }
    }

    protected override void UpdateDead()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shipManager.EnergyAmount += energy25;
        }
    }
}
