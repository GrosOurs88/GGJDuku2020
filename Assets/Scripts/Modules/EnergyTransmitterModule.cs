using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyTransmitterModule : Module
{
    public int maxEnergy100 = 3;
    public int maxEnergy50 = 1;
    public int maxEnergy25 = 0;

    public EnergyButtons buttons;

    public Image[] energyPoints;
    
    [SerializeField] private List<Module> connectedModules = new List<Module>();
    
    
    protected override void UpdateFullLife()
    {
        CheckNumberOfActiveModules(maxEnergy100);
    }

    protected override void UpdateDamaged()
    {
        CheckNumberOfActiveModules(maxEnergy50);
    }

    protected override void UpdateDead()
    {
        CheckNumberOfActiveModules(maxEnergy25);
    }


    protected override void Start()
    {
        base.Start();
        UpdateEnabledModulesUI();
        DisableUnusedButtons();
    }


    public void ToggleModule(int module)
    {
        if (currentHealth.Value != HealthEnum.HealthState.DEAD)
        {
            Module  moduleScript;
            switch (module)
            {
                case 0:
                    moduleScript = shipManager.GetModule(typeof(EngineModule));
                    break;
                case 1:
                    moduleScript = shipManager.GetModule(typeof(OxygenModule));
                    break;
                case 2:
                    moduleScript = shipManager.GetModule(typeof(EnergyGeneratorModule));
                    break;
                case 3:
                    moduleScript = shipManager.GetModule(typeof(InfoScreenModule));
                    break;
                case 4:
                    moduleScript = shipManager.GetModule(typeof(LightModule));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(module), module, null);
            }
            
            if (moduleScript.isPowered)
            {
                DisableModule(moduleScript);
            }
            else if (CanEnableModule(moduleScript))
            {
                EnableModule(moduleScript);
            }
        }

        switch (currentHealth.Value)
        {
            case HealthEnum.HealthState.FULL:
                CheckNumberOfActiveModules(maxEnergy100);
                break;
            case HealthEnum.HealthState.DAMAGED:
                CheckNumberOfActiveModules(maxEnergy50);
                break;
            case HealthEnum.HealthState.DEAD:
                CheckNumberOfActiveModules(maxEnergy25);
                break;
        }
        
        DisableUnusedButtons();
        UpdateEnabledModulesUI();
    }

    public void DisableAllModules()
    {
        for (var i = connectedModules.Count - 1; i >= 0; i--)
        {
            if (connectedModules[i].consumesEnergy)
                DisableModule(connectedModules[i]);
        }

        DisableUnusedButtons();
    }
    

    private void EnableModule(Module module)
    {
        module.PowerOn();
        connectedModules.Add(module);
    }

    private void DisableModule(Module module)
    {
        module.PowerOff();
        connectedModules.Remove(module);
    }
    
    private void CheckNumberOfActiveModules(int activesMax)
    {
        if (connectedModules.Count <= activesMax) return;
        
        while (connectedModules.Count > activesMax)
        {
            connectedModules[0].PowerOff();
            connectedModules.RemoveAt(0);
        }
    }

    private bool CanEnableModule(Module module)
    {
        return (module.consumesEnergy && shipManager.EnergyAmount > 0) || !module.consumesEnergy;
    }
    
    private void UpdateEnabledModulesUI()
    {
        foreach (var module in connectedModules)
        {
            switch (module)
            {
                case EnergyGeneratorModule energyGeneratorModule:
                    buttons.EnergyGenButton.colors = buttons.blockPowered;
                    break;
                case EngineModule engineModule:
                    buttons.EngineButton.colors = buttons.blockPowered;
                    break;
                case InfoScreenModule infoScreenModule:
                    buttons.InfoScreenButton.colors = buttons.blockPowered;
                    break;
                case LightModule lightModule:
                    buttons.LightButton.colors = buttons.blockPowered;
                    break;
                case OxygenModule oxygenModule:
                    buttons.OxygenButton.colors = buttons.blockPowered;
                    break;
            }
        }
    }
    
    
    private void DisableUnusedButtons()
    {
        if (!connectedModules.Exists(x => x.GetType() == typeof(EnergyGeneratorModule)))
        {
            buttons.EnergyGenButton.colors = buttons.blockUnpowered;
        }
        
        if (!connectedModules.Exists(x => x.GetType() == typeof(EngineModule)))
        {
            buttons.EngineButton.colors = buttons.blockUnpowered;
        }
        
        if (!connectedModules.Exists(x => x.GetType() == typeof(InfoScreenModule)))
        {
            buttons.InfoScreenButton.colors = buttons.blockUnpowered;
        }
        
        if (!connectedModules.Exists(x => x.GetType() == typeof(LightModule)))
        {
            buttons.LightButton.colors = buttons.blockUnpowered;
        }
        
        if (!connectedModules.Exists(x => x.GetType() == typeof(OxygenModule)))
        {
            buttons.OxygenButton.colors = buttons.blockUnpowered;
        }
    }

    
    
    

    [Serializable]
    public struct EnergyButtons
    {
        public Button EngineButton;
        public Button OxygenButton;
        public Button EnergyGenButton;
        public Button InfoScreenButton;
        public Button LightButton;

        public ColorBlock blockPowered;
        public ColorBlock blockUnpowered;
    }
}
