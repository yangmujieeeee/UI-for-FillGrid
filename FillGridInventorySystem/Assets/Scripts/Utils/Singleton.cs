using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型单例
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get => instance; }
    private static T instance;

    protected virtual void Awake()
    {
        if (instance == null)
            instance = (T)this;
        else
            Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
