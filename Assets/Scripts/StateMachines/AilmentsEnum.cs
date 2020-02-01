using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilmentsEnum : StateMachine<AilmentsEnum.Ailments>
{
    public AilmentsEnum(Ailments defaultValue) : base(defaultValue)
    {
        currentValue = defaultValue;
    }
    
    public enum Ailments
    {
        None,
        Fire,
        ElectricBug
    }

    protected override void EnterState(Ailments state)
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitState(Ailments state)
    {
        throw new System.NotImplementedException();
    }

   
}