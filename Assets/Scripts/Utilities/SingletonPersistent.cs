using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance {get => _instance;}

    private void OnDestroy() {
        if(_instance == this) _instance = null;
    } 

    protected virtual void Awake() {
        if(_instance == null){
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }
}
