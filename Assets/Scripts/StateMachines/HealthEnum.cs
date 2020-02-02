using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
    
    
    public event StateChangeEvent EnterFULL;
    public event StateChangeEvent EnterDAMAGED;
    public event StateChangeEvent EnterDEAD;
    
    protected override void EnterState(HealthEnum.HealthState state)
    {
        switch (state)
        {
            case HealthEnum.HealthState.FULL:
                EnterFULL?.Invoke();
                break;
            case HealthEnum.HealthState.DAMAGED:
                EnterDAMAGED?.Invoke();
                break;
            case HealthEnum.HealthState.DEAD:
                EnterDEAD?.Invoke();
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
