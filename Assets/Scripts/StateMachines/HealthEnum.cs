using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnum : StateMachine<HealthEnum.HealthState>
{
    public HealthEnum(HealthState defaultValue) : base(defaultValue)
    {
        currentValue = defaultValue;
    }
    
    public enum HealthState
    {
        FULL,
        DAMAGED,
        DEAD
    }
    
    protected override void EnterState(HealthEnum.HealthState state)
    {
        switch (state)
        {
            case HealthEnum.HealthState.FULL:
                break;
            case HealthEnum.HealthState.DAMAGED:
                break;
            case HealthEnum.HealthState.DEAD:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    protected override void ExitState(HealthEnum.HealthState state)
    {
        switch (state)
        {
            case HealthEnum.HealthState.FULL:
                break;
            case HealthEnum.HealthState.DAMAGED:
                break;
            case HealthEnum.HealthState.DEAD:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }    }


}
