using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightChange : MonoBehaviour
{
    private float startValue;

    private Light l;
    
    private void Start()
    {
        l = GetComponent<Light>();
        startValue = l.intensity;
    }

    public void ChangeLightValue(float ratio)
    {
        l.intensity = startValue * ratio;
    }
}
