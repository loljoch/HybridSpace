using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DontDestroyMe : MonoBehaviour
{
    public UnityEvent endLevel;
    
    private void Awake() {
        DontDestroyOnLoad(this);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            endLevel?.Invoke();
        }
    }
}
