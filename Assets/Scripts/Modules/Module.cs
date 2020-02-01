using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour
{
    private ShipManager shipManager => ShipManager.instance;

    

    public HealthEnum currentHealth = new HealthEnum(HealthEnum.HealthState.FULL);
    public AilmentsEnum currentAilment = new AilmentsEnum(AilmentsEnum.Ailments.None);
    
    
    private float lifePoints;

    public float LifePoints
    {
        get => lifePoints;
        set
        {
            lifePoints -= value;
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

    public float damageAmount = 1;
    
    public bool requireEnergy = false;

    public bool consumesEnergy = false;

    public bool canBeDamaged = true;



    // UNITY FUNCTIONS
    private void OnEnable()
    {
        ShipManager.ModulesUpdate += UpdateModule;
    }

    private void OnDisable()
    {
        ShipManager.ModulesUpdate -= UpdateModule;
    }
    
    
    
    // FUNCTIONS
    public virtual void DamageModule(float value)
    {
        LifePoints -= value;
    }

    private void UpdateModule()
    {
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
    }
    

    protected abstract void UpdateFullLife();
    protected abstract void UpdateDamaged();
    protected abstract void UpdateDead();
    

}