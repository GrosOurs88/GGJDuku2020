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

    private FloatParameter vignetteBase;
    private PostProcessVolume postProcessVolume;

    private void Awake()
    {
        instance = this;
        postProcessVolume = cameraControl.GetComponent<PostProcessVolume>();
        vignetteBase = postProcessVolume.sharedProfile.GetSetting<Vignette>().intensity;
    }

    private void Update()
    {
        FloatParameter newValue = new FloatParameter
        {
            value = Mathf.Lerp(1, vignetteBase, ((shipManager.OxygenAmount)*2) / 100f) // start reducing at 50% oxygen
        };

        postProcessVolume.sharedProfile.AddSettings<Vignette>().intensity = newValue;
    }
}
