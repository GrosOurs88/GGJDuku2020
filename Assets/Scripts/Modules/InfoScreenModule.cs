using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScreenModule : Module
{
    public GameObject[] infoScreens;

    protected override void Start()
    {
        base.Start();
        
        currentHealth.EnterFULL += () => GlitchLevel(0);
        currentHealth.EnterDAMAGED += () => GlitchLevel(1);
        currentHealth.EnterDEAD += () => GlitchLevel(2);
    }


    public override void PowerOn()
    {
        base.PowerOn();

        foreach (var screen in infoScreens)
        {
            screen.SetActive(true);
        }
    }

    public override void PowerOff()
    {
        base.PowerOff();
        
        foreach (var screen in infoScreens)
        {
            screen.SetActive(false);
        }
    }


    private void GlitchLevel(int state)
    {
        //TODO: les glitchs
    }
    
}
