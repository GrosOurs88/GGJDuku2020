using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    public GameManager gameManager => GameManager.instance;

    public GameObject ship;

    
    [SerializeField] private float hullPoints;
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


    [SerializeField] private float oxygenAmount;
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

    
    [SerializeField] private float energyAmount;
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
    
    [SerializeField] private float temperatureAmount;
    public float TemperatureAmount
    {
        get => temperatureAmount;
        set
        {
            temperatureAmount = value;
            if (temperatureAmount > 100)
            {
                temperatureAmount = 100;
            }
            
            else if (temperatureAmount < 0)
            {
                temperatureAmount = 0;
            }
        }
    }
    
    

    [SerializeField] private List<Module> ModuleList = new List<Module>();


    public delegate void ShipUpdateEvents();
    public static event ShipUpdateEvents ModulesUpdate; 
    

    // UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
        InitShip();
    }


    private void Update()
    {
        LooseOxygen();
        HeatUpdate();
        CheckDamage();
        
        ModulesUpdate?.Invoke(); 
    }


    // GAMEPLAY FUNCTIONS
    private void LooseOxygen()
    {
        OxygenAmount -= gameManager.oxygenLossSpeed * Time.deltaTime;
    }

    private void HeatUpdate()
    {
        TemperatureAmount += gameManager.temperatureGainSpeed * Time.deltaTime;
    }
    
    private void CheckDamage()
    {
        float t = Time.deltaTime;
        
        foreach (Module module in ModuleList)
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
        foreach (var module in ModuleList)
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
        EnergyAmount = 100;
        TemperatureAmount = 100;
    }
    
    private void InitModule(Module module)
    {
        module.LifePoints = 100;
    }

    public void RegisterModule(Module module)
    {
        ModuleList.Add(module);
        InitModule(module);
    }
    
    public void PowerModule(Type moduleType)
    {
        var module = ModuleList.Find(y => y.GetType() == moduleType);

        if (module.isPowered)
            module.PowerOff();
        else
            module.PowerOn();
        
    }
}
