using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Class Implementing a Generic Singelton
/// </summary>
/// <typeparam name="T"></typeparam>
public class PersistentGameObjectSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static PersistentGameObjectSingleton<T> Instance { get; private set; }
  
    void Awake()
    {

        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GlobalManager.
            DestroyImmediate(gameObject);

            return;
        }
    }
}