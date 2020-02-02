using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightModule : Module
{
    public LightChange[] lights;
    
    protected override void UpdateFullLife()
    {
        //throw new System.NotImplementedException();
    }

    protected override void UpdateDamaged()
    {
        //throw new System.NotImplementedException();
    }

    protected override void UpdateDead()
    {
        //throw new System.NotImplementedException();
    }

    private void Start()
    {
        currentHealth.EnterFULL += () => SetLight(1);
        currentHealth.EnterDAMAGED += () => SetLight(0.5f);
        currentHealth.EnterDEAD += () => SetLight(0);
    }

    private void SetLight(float ratio)
    {
        foreach (var ampoule in lights)
        {
            ampoule.ChangeLightValue(ratio);
        }
    }

}
