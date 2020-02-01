using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    public GameManager gameManager => GameManager.instance;
    
    
    public float hullPoints = 100;

    public float oxygenAmount = 100;
    
    public Module[] moduleList;


    public delegate void ShipUpdateEvents();

    public static event ShipUpdateEvents ModulesUpdate; 

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        LooseOxygen();

        ModulesUpdate?.Invoke(); 
    }


    private void LooseOxygen()
    {
        oxygenAmount -= gameManager.oxygenLossSpeed * Time.deltaTime;
    }
    
    private void CheckDamage()
    {
        float t = Time.deltaTime;
        
        foreach (Module module in moduleList)
        {
            if (module.LifePoints <= 0)
            {
                hullPoints -= module.damageAmount * t;
            }
        }
    }
    
}
