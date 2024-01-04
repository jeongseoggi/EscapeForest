using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T : SingleTon<T>
{
    public static T instance;
    protected void Awake()
    { 

        if (instance == null)
            instance = (T)this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
