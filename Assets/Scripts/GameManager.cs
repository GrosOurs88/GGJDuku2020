using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public float oxygenLossSpeed = 1;

    private void Awake()
    {
        instance = this;
    }
}
