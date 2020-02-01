using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    public GameManager gameManager => GameManager.instance;

    public GameObject ship;

    
    private float hullPoints;
    public float HullPoints
    {
        get => hullPoints;
        set
        {
            hullPoints = value;
            if (hullPoints > 100)
            {
                hullPoints = 100;
            }
            
            else if (hullPoints < 0)
            {
                hullPoints = 0;
            }
        }
    }


    private float oxygenAmount;
    public float OxygenAmount
    {
        get => oxygenAmount;
        set
        {
            oxygenAmount = value;
            if (oxygenAmount > 100)
            {
                oxygenAmount = 100;
            }
            
            else if (oxygenAmount < 0)
            {
                oxygenAmount = 0;
            }
        }
    }

    
    private float energyAmount;
    public float EnergyAmount
    {
        get => energyAmount;
        set
        {
            energyAmount = value;
            if (energyAmount > 100)
            {
                energyAmount = 100;
            }
            
            else if (energyAmount < 0)
            {
                energyAmount = 0;
            }
        }
    }
    
    

    public Module[] moduleList;


    public delegate void ShipUpdateEvents();
    public static event ShipUpdateEvents ModulesUpdate; 
    

    // UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
        InitShip();
        InitModules();
    }


    private void Update()
    {
        LooseOxygen();

        ModulesUpdate?.Invoke(); 
    }


    // GAMEPLAY FUNCTIONS
    private void LooseOxygen()
    {
        OxygenAmount -= gameManager.oxygenLossSpeed * Time.deltaTime;
    }
    
    private void CheckDamage()
    {
        float t = Time.deltaTime;
        
        foreach (Module module in moduleList)
        {
            if (module.LifePoints <= 0)
            {
                HullPoints -= module.damageAmount * t;
            }
        }
    }

    public void MoveShip(float amount)
    {
        ship.transform.position += new Vector3(0f, 0f, amount * Time.deltaTime);
    }

    private void OutOfEnergy()
    {
        foreach (var module in moduleList)
        {
            if (module.consumesEnergy)
            {
                module.PowerOff();
            }
        }
    }
    
    
    // INIT
    private void InitShip()
    {
        OxygenAmount = 100;
    }
    
    private void InitModules()
    {
        foreach (var module in moduleList)
        {
            module.LifePoints = 100; 
        }
    }
    
}
