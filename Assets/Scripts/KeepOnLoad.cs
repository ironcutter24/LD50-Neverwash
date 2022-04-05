using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class KeepOnLoad : Singleton<KeepOnLoad>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
