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
    
    private List<Module> connectedModules = new List<Module>();
    
    
    protected override void UpdateFullLife()
    {
        if (connectedModules.Count > maxEnergy100)
        {
            for (int i = connectedModules.Count; i > maxEnergy100; i--)
            {
                connectedModules[i-1].PowerOff();
                connectedModules.RemoveAt(i-1);
            }
        }
    }

    protected override void UpdateDamaged()
    {
        if (connectedModules.Count > maxEnergy50)
        {
            for (int i = connectedModules.Count; i > maxEnergy50; i--)
            {
                connectedModules[i-1].PowerOff();
                connectedModules.RemoveAt(i-1);
            }
        }
    }

    protected override void UpdateDead()
    {
        if (connectedModules.Count > maxEnergy25)
        {
            for (int i = connectedModules.Count; i > maxEnergy25; i--)
            {
                connectedModules[i-1].PowerOff();
                connectedModules.RemoveAt(i-1);
            }
        }
    }

    public void EnableModule(dynamic module)
    {
        if (currentHealth.Value != HealthEnum.HealthState.DEAD)
        {
            switch ((int) module)
            {
                case 0:
                    shipManager.PowerModule(typeof(EngineModule));
                    break;
                case 1:
                    shipManager.PowerModule(typeof(OxygenModule));
                    break;
                case 2:
                    shipManager.PowerModule(typeof(EnergyGeneratorModule));
                    break;
                case 3:
                    shipManager.PowerModule(typeof(InfoScreenModule));
                    break;
                case 4:
                    shipManager.PowerModule(typeof(LightModule));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(module), module, null);
            }
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

        public Image PowerAvailableImage;
    }
}
