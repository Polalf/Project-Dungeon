using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    public bool dontDestroyOnLoad;
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this as T;

        if (dontDestroyOnLoad)
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}
