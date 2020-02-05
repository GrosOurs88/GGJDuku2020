using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomDisfunctionsManager : MonoBehaviour
{
    public float baseTimeBetweenRolls = 20;
    public float additionalRandom;

    [Range(0, 1)] public float disfunctionChance = 0.2f;


    public bool disfunctionDamage;
    public float disfunctionMinDamage;
    public float disfunctionMaxDamage;


    public Module[] targetModules;


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
        if (UnityEngine.Random.Range(0f, 1f) > disfunctionChance) return; // no disfunction


        List<Module> targets = new List<Module>(targetModules); // Get and shuffles targetable modules
        targets.Shuffle();

        while (targets.Count > 0)
        {
            if (!targets[0].IsOnFire || !targets[0].IsOnElec) // found a targetable module
            {
                float damage = disfunctionDamage
                    ? UnityEngine.Random.Range(disfunctionMinDamage, disfunctionMaxDamage)
                    : 0;

                targets[0].DamageModule(damage, 1);

                break;
            }
            targets.RemoveAt(0);
        }
    }
}
