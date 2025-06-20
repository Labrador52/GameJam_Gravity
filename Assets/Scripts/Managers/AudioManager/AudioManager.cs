using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IManager
{
    public IEnumerable<Type> GetDependencies()
    {
        yield break;
    }

    public void Initialize()
    {
        Debug.Log("AudioManager initialized");
    }
}