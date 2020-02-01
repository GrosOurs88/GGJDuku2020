using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    public GameManager gameManager => GameManager.instance;

    public GameObject ship;
    
    public float hullPoints = 100;

    public float oxygenAmount = 100;
    
    public Module[] moduleList;


    public delegate void ShipUpdateEvents();

    public static event ShipUpdateEvents ModulesUpdate; 

    private void Awake()
    {
        instance = this;
        InitModules();
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

    public void MoveShip(float amount)
    {
        ship.transform.position += new Vector3(0f, 0f, amount * Time.deltaTime);
    }
    
    
    private void InitModules()
    {
        foreach (var module in moduleList)
        {
            module.LifePoints = 100; 
        }
    }
    
}
