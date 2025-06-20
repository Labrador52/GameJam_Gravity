using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager : MonoBehaviour, IManager
{
    public abstract IEnumerable<Type> GetDependencies();

    public virtual void Initialize()
    {
        Debug.Log($"{GetType().Name} initialized");
    }
}