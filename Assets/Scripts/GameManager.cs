using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ShipManager shipManager => ShipManager.instance;

    public Camera_Control cameraControl;
    
    public float oxygenLossSpeed = 1;
    public float temperatureGainSpeed = 1;
    public float shipSpeed = 10;
    public float shipSpeedSmoothSlow = 0.5f;
    public float shipSpeedSmoothAcceleration = 0.2f;
    public float shipRotationSpeed = 10;
    public float distanceOnPathOrientation = 0.01f;

    public Tool.Tools currentTool = Tool.Tools.None;
    public float toolTapeRepairValue = 2;
    public float toolExtinguisherRepairValue = 1;
    public float toolElectricFixerRepairValue = 1;

    public float toolTapeUse = 5;
    public float toolFireExtUse = 5;
    public float toolElectricFixerUse = 5;

    public float fireDamageOverTime = 1;

    private float vignetteBase;
    private Vignette vignetteLayer;

    public float CurrentToolDurability
    {
        get => currentToolDurability;
        set
        {
            currentToolDurability = value;

            if (currentToolDurability > 100)
            {
                currentToolDurability = 100;
            }

            if (currentToolDurability < 0)
            {
                currentToolDurability = 0;
            }

        }
    }
    private float currentToolDurability;

    private void Awake()
    {
        instance = this;
        vignetteLayer = cameraControl.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();
        vignetteBase = vignetteLayer.intensity;
    }

    private void Update()
    {
        vignetteLayer.intensity.value =
            Mathf.Lerp(1f, vignetteBase, ((shipManager.OxygenAmount) * 2) / 100f); // start reducing at 50% oxygen
    }

    private void OnApplicationQuit()
    {
        vignetteLayer.intensity.value = vignetteBase;
    }
}
