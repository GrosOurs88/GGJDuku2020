using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{

    public float lifePoints = 100;

    public Ailments currentAilment = Ailments.None;

    public bool requireEnergy = false;

    public bool consumesEnergy = false;

    public bool canBeDamaged = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Ailments
{
    None,
    Fire,
}
