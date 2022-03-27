using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSingleton : MonoBehaviour
{
    public static ParticleSystemSingleton instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
