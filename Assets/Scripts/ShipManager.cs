using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    public GameManager gameManager => GameManager.instance;

    public GameObject ship;

    public DOTweenPath path;
    private Tween tween;

    
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
            if (energyAmount > 0 && value <= 0)
            {
                OutOfEnergy();
            }
            
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

    public float distanceTraveled;
    public float targetSpeed;
    
    [SerializeField] private List<Module> ModuleList = new List<Module>();


    public delegate void ShipUpdateEvents();
    public static event ShipUpdateEvents ModulesUpdate; 
    

    // UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
        InitShip();
    }

    private void Start()
    {
        tween = path.GetTween();
        tween.timeScale = 0;
        targetSpeed = 0;
    }


    private void Update()
    {
        LooseOxygen();
        HeatUpdate();
        CheckDamage();
        MoveShip();
        RotateShip();
        
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

    public void SetTargetSpeed(float amount)
    {
        targetSpeed = amount;
    }
    
    private void MoveShip()
    {
        if (tween.timeScale < targetSpeed) //acceleration
        {
            tween.timeScale = Mathf.MoveTowards(tween.timeScale, targetSpeed,
                gameManager.shipSpeedSmoothAcceleration * Time.deltaTime);
        }
        else if (tween.timeScale > targetSpeed) //slow down
        {
            tween.timeScale = Mathf.MoveTowards(tween.timeScale, targetSpeed,
                gameManager.shipSpeedSmoothSlow * Time.deltaTime);
        }
        
        distanceTraveled = tween.fullPosition;
    }

    private void RotateShip()
    {
        var shipTransform = path.transform;

        var aimPoint = tween.PathGetPoint(tween.ElapsedDirectionalPercentage() + gameManager.distanceOnPathOrientation);
        //aimPoint = aimPoint - transform.position;
        /*shipTransform.forward = Vector3.MoveTowards(shipTransform.forward, aimPoint - shipTransform.position,
            tween.timeScale * gameManager.shipRotationSpeed * Time.deltaTime);*/
        aimPoint = Vector3.MoveTowards(shipTransform.position + shipTransform.forward * Vector3.Distance(shipTransform.position, aimPoint), aimPoint,
            tween.timeScale * gameManager.shipRotationSpeed * Time.deltaTime);
        shipTransform.LookAt(aimPoint);

    }

    private void OutOfEnergy()
    {
        ((EnergyTransmitterModule) GetModule(typeof(EnergyTransmitterModule))).DisableAllModules();
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
    

    public Module GetModule(Type moduleType)
    {
        return ModuleList.Find(y => y.GetType() == moduleType);
    }
    
    public Module[] GetModules(Type moduleType)
    {
        return ModuleList.FindAll(y => y.GetType() == moduleType).ToArray();
    }
}
