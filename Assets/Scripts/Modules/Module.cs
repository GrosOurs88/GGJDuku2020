﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class Module : MonoBehaviour
{
    protected ShipManager shipManager => ShipManager.instance;
    protected GameManager gameManager => GameManager.instance;
    
    
    public HealthEnum currentHealth = new HealthEnum(HealthEnum.HealthState.FULL);


    public delegate void StateChangeEvent();

    public event StateChangeEvent EnterFire;
    public event StateChangeEvent ExitFire;
    public event StateChangeEvent EnterElec;
    public event StateChangeEvent ExitElec;


    private bool isOnFire;
    public bool IsOnFire
    {
        get => isOnFire;
        set
        {
            if (value != isOnFire)
            {
                if (value == true)
                    EnterFire?.Invoke();
                else
                    ExitFire?.Invoke();

                isOnFire = value;
            }
        }
    }


    private bool isOnElec;
    public bool IsOnElec
    {
        get => isOnElec;
        set
        {
            if (value != isOnElec)
            {
                if (value == true)
                    EnterElec?.Invoke();
                else
                    ExitElec?.Invoke();

                isOnElec = value;
            }
        }
    }


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
    public float fireDifficulty = 3;
    public float fireDifficultyRandom = 1;
    public float elecDifficulty = 5;
    public float elecDifficultyRandom = 2;

    private float fireHealth;
    private float elecHealth;
    private Coroutine fireCoroutine;
    private Coroutine elecCoroutine;
    
    public GameObject fire;
    public GameObject elec;


    // UNITY FUNCTIONS
    protected virtual void OnEnable()
    {
        ShipManager.ModulesUpdate += UpdateModule;
        shipManager.RegisterModule(this);

        // AILMENTS EVENTS
        EnterFire +=    () => fire.SetActive(true);
        EnterFire +=    () => fireHealth = fireDifficulty + UnityEngine.Random.Range(0f, fireDifficultyRandom);
        ExitFire +=     () => fire.SetActive(false);
        
        EnterElec +=    () => elec.SetActive(true);
        EnterElec +=    () => elecHealth = elecDifficulty + UnityEngine.Random.Range(0f, elecDifficultyRandom);
        ExitElec +=     () => elec.SetActive(false);
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
    public virtual bool DamageModule(float value, float ailmentMultiplier = 0, AilmentsEnum.Ailments ailment = AilmentsEnum.Ailments.NONE)
    {
        LifePoints -= value;
        
        return RollAilment(ailmentMultiplier, ailment);
    }

    protected virtual bool RollAilment(float ailmentMultiplier = 0, AilmentsEnum.Ailments ailment = AilmentsEnum.Ailments.NONE)
    {
        float rand = UnityEngine.Random.Range(0f, 1f);

        if (rand < chanceForAilment + ailmentMultiplier || chanceForAilment + ailmentMultiplier >= 1) //gets ailment
        {
            AilmentsEnum.Ailments target;

            if (ailment != AilmentsEnum.Ailments.NONE)
                target = ailment;
            else
            {
                int r = UnityEngine.Random.Range(0, 2);
                
                if (r == 0 && !isOnFire)
                {
                    target = canUseFire ? AilmentsEnum.Ailments.FIRE : AilmentsEnum.Ailments.ELECTRICBUG;
                }
                else if (!isOnElec)
                {
                    target = canUseElec ? AilmentsEnum.Ailments.ELECTRICBUG : AilmentsEnum.Ailments.FIRE;
                }
                else
                {
                    target = AilmentsEnum.Ailments.NONE;
                }
                
            }

            switch (target)
            {
                case AilmentsEnum.Ailments.FIRE:
                    IsOnFire = true;
                    return true;
                    break;
                case AilmentsEnum.Ailments.ELECTRICBUG:
                    IsOnElec = true;
                    return true;
                    break;
                case AilmentsEnum.Ailments.NONE:
                    return false;
                    break;
            }
        }

        return false;
    }

    private void UpdateModule()
    {
        // DAMAGE SHIP
        if (LifePoints <= 0) 
            shipManager.HullPoints -= damageAmount * Time.deltaTime;

        
        // NEED POWER ?
        if (!isPowered && requireEnergy)
        {
            UpdateUnpowered();
            return;
        }// Non branché



        int nbOfAilments = 0;

        if (IsOnFire)
            nbOfAilments++;
        if (isOnElec)
            nbOfAilments++;

            // MODULE UPDATE
        if (LifePoints >= 50 && nbOfAilments == 0)
        {
            UpdateFullLife();
        }
        else if ((LifePoints >= 25 && nbOfAilments == 0) || (LifePoints >= 50 && nbOfAilments == 1))
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

    protected virtual void UpdateUnpowered()
    {
        
    }

    public void OnClick() //try fix module
    {
        switch (gameManager.currentTool)
        {
            case Tool.Tools.Tape:
                LifePoints += gameManager.toolTapeValue;
                break;
            case Tool.Tools.FireExtinguisher:
                if (fireCoroutine == null)
                    fireCoroutine = StartCoroutine(FireExtinguish());
                break;
            case Tool.Tools.ElectricFixer:
                if (elecCoroutine == null)
                    elecCoroutine = StartCoroutine(ElecFix());
                break;
        }
    }

    private IEnumerator FireExtinguish()
    {
        while (IsOnFire && Input.GetMouseButton(0))
        {
            fireHealth -= gameManager.toolExtinguisherValue * Time.deltaTime;

            if (fireHealth <= 0)
            {
                IsOnFire = false;
                break;
            }

            yield return null;
        }
    }

    private IEnumerator ElecFix()
    {
        while (IsOnElec && Input.GetMouseButton(0))
        {
            elecHealth -= gameManager.toolElectricFixerValue * Time.deltaTime;

            if (elecHealth <= 0)
            {
                IsOnElec = false;
                break;
            }

            yield return null;
        }
    }
}