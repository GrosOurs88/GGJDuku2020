using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipUIUpdater : MonoBehaviour
{
    ShipManager shipManager => ShipManager.instance;
    
    public TextMeshPro shipHull;
    public TextMeshPro shipOxygen;
    public TextMeshPro shipTemperature;
    public TextMeshPro shipEnergy;
    public TextMeshPro shipGlass;
    
    
    public TextMeshPro shipEngine;
    public TextMeshPro shipLights;
    public TextMeshPro shipEnergyRepartitor;
    public TextMeshPro shipGenerator;
    public TextMeshPro shipOxygenGenerator;


    public float refreshTimer = 1;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= refreshTimer)
        {
            timer -= refreshTimer;
            SlowUpdate();
        }
    }

    private void SlowUpdate()
    {
        // RESOURCES
        shipHull.text = shipManager.HullPoints.ToString("F0");
        shipOxygen.text = shipManager.OxygenAmount.ToString("F0");
        shipTemperature.text = shipManager.TemperatureAmount.ToString("F0");
        shipEnergy.text = shipManager.EnergyAmount.ToString("F0");

        // MODULES
        shipEngine.text = shipManager.GetModule(typeof(EngineModule)).LifePoints.ToString("F0");
        shipLights.text = shipManager.GetModule(typeof(LightModule)).LifePoints.ToString("F0");
        shipEnergyRepartitor.text = shipManager.GetModule(typeof(EnergyTransmitterModule)).LifePoints.ToString("F0");
        shipGenerator.text = shipManager.GetModule(typeof(EnergyGeneratorModule)).LifePoints.ToString("F0");
        shipOxygenGenerator.text = shipManager.GetModule(typeof(OxygenModule)).LifePoints.ToString("F0");
        
        
        // GLASS AVERAGE
        float averageGlassValue = 0;
        var glasses = shipManager.GetModules(typeof(GlassModule));
        foreach (var module in glasses)
        {
            averageGlassValue += module.LifePoints;
        }

        averageGlassValue /= glasses.Length;

        shipGlass.text = averageGlassValue.ToString("F0");
    }
}
