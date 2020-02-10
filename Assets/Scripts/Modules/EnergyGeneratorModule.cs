using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorModule : Module
{
    public float energy100 = 5;
    public float energy50 = 2;
    public float energy25 = 0;
    

    public override void OnClick()
    {
        base.OnClick();

        if (requireEnergy && !isPowered) return;

        float energyIncrease = 0;

        switch (currentHealth.Value)
        {
            case HealthEnum.HealthState.FULL:
                energyIncrease = energy100;
                break;
            case HealthEnum.HealthState.DAMAGED:
                energyIncrease = energy50;
                break;
            case HealthEnum.HealthState.DEAD:
                energyIncrease = energy25;
                break;
        }

        shipManager.EnergyAmount += energyIncrease;
    }
}
