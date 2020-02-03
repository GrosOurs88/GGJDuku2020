using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDisfunctionsManager : MonoBehaviour
{
    public float baseTimeBetweenRolls = 20;
    public float additionalRandom;

    [Range(0,1)] public float disfunctionPercent = 0.2f;

    private float timer;


    private void Start()
    {
        timer = RollNewTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer += RollNewTimer();
            RollDisfunction();
        }
    }



    private float RollNewTimer()
    {
        return baseTimeBetweenRolls + UnityEngine.Random.Range(-additionalRandom, additionalRandom);
    }

    private void RollDisfunction()
    {
        if (UnityEngine.Random.Range(0f, 1f) > disfunctionPercent) return;

    }
}
