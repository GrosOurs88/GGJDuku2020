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

    private float vignetteBase;
    private Vignette vignetteLayer;
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
