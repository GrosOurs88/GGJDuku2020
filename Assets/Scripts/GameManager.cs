using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public float oxygenLossSpeed = 1;
    public float shipSpeed = 10;

    private void Awake()
    {
        instance = this;
    }
}
