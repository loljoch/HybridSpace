using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public bool triggered = false;
    public bool triggerOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        onTriggerEnter?.Invoke();

        if (triggerOnce)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
