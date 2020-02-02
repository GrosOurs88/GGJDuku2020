﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineModule : Module
{
    public float speedRatio100 = 1;
    public float speedRatio50 = 0.5f;
    public float speedratio25 = 0;

    public GameObject[] engines;

    protected override void UpdateFullLife()
    {
        shipManager.MoveShip(gameManager.shipSpeed * speedRatio100);
    }

    protected override void UpdateDamaged()
    {
        shipManager.MoveShip(gameManager.shipSpeed * speedRatio50);
    }

    protected override void UpdateDead()
    {
        shipManager.MoveShip(gameManager.shipSpeed * speedratio25);
    }

    
    
    public override void PowerOn()
    {
        base.PowerOn();

        foreach (var engine in engines)
        {
            engine.SetActive(true);
        }
    }
    
    public override void PowerOff()
    {
        base.PowerOff();

        foreach (var engine in engines)
        {
            engine.SetActive(false);
        }
    }
}
