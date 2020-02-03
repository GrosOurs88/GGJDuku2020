using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class StateMachine<T> where T : struct, IConvertible
{
    [SerializeField] protected T currentValue;
    public delegate void StateChangeEvent();

    public T Value
    {
        get => currentValue;
        set
        {
            if (!currentValue.Equals(value))
            {
                ExitState(currentValue);
                currentValue = value;
                EnterState(currentValue);
            }
        }
    }


    public StateMachine(T defaultValue)
    {
        currentValue = defaultValue;
    }
    
    
    protected abstract void EnterState(T state);
    protected abstract void ExitState(T state);
}
