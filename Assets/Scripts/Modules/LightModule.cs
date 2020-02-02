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

    protected override void Start()
    {
        base.Start();
        
        currentHealth.EnterFULL += () => SetLights(1);
        currentHealth.EnterDAMAGED += () => SetLights(0.5f);
        currentHealth.EnterDEAD += () => SetLights(0);
    }

    private void SetLights(float ratio)
    {
        foreach (var ampoule in lights)
        {
            ampoule.ChangeLightValue(ratio);
        }
    }
    
    public override void PowerOn()
    {
        base.PowerOn();

        foreach (var ampoule in lights)
        {
            ampoule.gameObject.SetActive(true);
        }
    }

    public override void PowerOff()
    {
        base.PowerOff();
        
        foreach (var ampoule in lights)
        {
            ampoule.gameObject.SetActive(false);
        }
    }

}
