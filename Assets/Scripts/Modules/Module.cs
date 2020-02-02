using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class Module : MonoBehaviour
{
    protected ShipManager shipManager => ShipManager.instance;
    protected GameManager gameManager => GameManager.instance;
    
    
    public HealthEnum currentHealth = new HealthEnum(HealthEnum.HealthState.FULL);
    public AilmentsEnum currentAilment = new AilmentsEnum(AilmentsEnum.Ailments.NONE);
    
    
    // GAMEPLAY VARIABLES
    [SerializeField] private float lifePoints;
    public float LifePoints
    {
        get => lifePoints;
        set
        {
            lifePoints = value;
            if (lifePoints <= 0)
            {
                lifePoints = 0;
            }
            else if (lifePoints >= 100)
            {
                lifePoints = 100;
            }

            if (lifePoints >= 50)
            {
                currentHealth.Value = HealthEnum.HealthState.FULL;
            }
            else if (lifePoints >= 25)
            {
                currentHealth.Value = HealthEnum.HealthState.DAMAGED;
            }
            else
            {
                currentHealth.Value = HealthEnum.HealthState.DEAD;
            }
            
            
        }
    }
    
    
    public bool isPowered = false;

    
    // PARAMS
    public float damageAmount = 1;
    
    public bool requireEnergy = false;

    public bool consumesEnergy = false;
    public float energyCost = 2;

    public bool canBeDamaged = true;

    public float chanceForAilment = 0.2f;

    public bool canUseFire = true;
    public bool canUseElec;
    
    public GameObject fire;
    public GameObject elec;


    // UNITY FUNCTIONS
    protected virtual void OnEnable()
    {
        ShipManager.ModulesUpdate += UpdateModule;
        shipManager.RegisterModule(this);

        // AILMENTS STATE MACHINE
        currentAilment.EnterFIRE += () => fire.SetActive(true);
        currentAilment.ExitFIRE += () => fire.SetActive(false);
        
        currentAilment.EnterELECTRIC += () => elec.SetActive(true);
        currentAilment.ExitELECTRIC += () => elec.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        ShipManager.ModulesUpdate -= UpdateModule;
    }

    protected virtual void Start()
    {
        if (!isPowered)
            PowerOff();
    }


    // FUNCTIONS
    public virtual void DamageModule(float value, float ailmentMultiplier = 0, AilmentsEnum.Ailments ailment = AilmentsEnum.Ailments.NONE)
    {
        LifePoints -= value;
        
        RollAilment(ailmentMultiplier, ailment);
    }

    protected virtual void RollAilment(float ailmentMultiplier = 0, AilmentsEnum.Ailments ailment = AilmentsEnum.Ailments.NONE)
    {
        float rand = UnityEngine.Random.Range(0f, 1f);

        if (rand > chanceForAilment + ailmentMultiplier) //gets ailment
        {
            AilmentsEnum.Ailments target;

            if (ailment != AilmentsEnum.Ailments.NONE)
                target = ailment;
            else
            {
                int r = UnityEngine.Random.Range(0, 1);
                
                if (r == 0)
                {
                    target = canUseFire ? AilmentsEnum.Ailments.FIRE : AilmentsEnum.Ailments.ELECTRICBUG;
                }
                else
                {
                    target = canUseElec ? AilmentsEnum.Ailments.ELECTRICBUG : AilmentsEnum.Ailments.FIRE;
                }
                
            }

            currentAilment.Value = target;
        }
    }

    private void UpdateModule()
    {
        // DAMAGE SHIP
        if (LifePoints <= 0) 
            shipManager.HullPoints -= damageAmount * Time.deltaTime;

        
        // NEED POWER ?
        if (!isPowered && requireEnergy) // Non branché
            return;
        
        
        // MODULE UPDATE
        if (LifePoints >= 50)
        {
            UpdateFullLife();
        }
        else if (LifePoints >= 25)
        {
            UpdateDamaged();
        }
        else
        {
            UpdateDead();
        }

        // ENERGY CONSUMPTION
        if (consumesEnergy)
            shipManager.EnergyAmount -= energyCost * Time.deltaTime;
        
    }

    public virtual void PowerOn()
    {
        isPowered = true;
    }
    
    public virtual  void PowerOff()
    {
        isPowered = false;
    }
    

    protected abstract void UpdateFullLife();
    protected abstract void UpdateDamaged();
    protected abstract void UpdateDead();
    

}