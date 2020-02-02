using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AilmentsEnum : StateMachine<AilmentsEnum.Ailments>
{
    public AilmentsEnum(Ailments defaultValue) : base(defaultValue)
    {
        currentValue = defaultValue;
    }
    
    public enum Ailments
    {
        NONE,
        FIRE,
        ELECTRICBUG
    }
    
    public event StateChangeEvent EnterFIRE;
    public event StateChangeEvent EnterELECTRIC;
    public event StateChangeEvent EnterNONE;
    
    public event StateChangeEvent ExitFIRE;
    public event StateChangeEvent ExitELECTRIC;
    public event StateChangeEvent ExitNONE;

    protected override void EnterState(Ailments state)
    {
        switch (state)
        {
            case Ailments.NONE:
                EnterNONE?.Invoke();
                break;
            case Ailments.FIRE:
                EnterFIRE?.Invoke();
                break;
            case Ailments.ELECTRICBUG:
                EnterELECTRIC?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    protected override void ExitState(Ailments state)
    {
        switch (state)
        {
            case Ailments.NONE:
                ExitNONE?.Invoke();
                break;
            case Ailments.FIRE:
                ExitFIRE?.Invoke();
                break;
            case Ailments.ELECTRICBUG:
                ExitELECTRIC?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

   
}