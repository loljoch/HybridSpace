using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public bool triggerOnce = true;
    private int timesTriggered = 0;
    private BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        timesTriggered += 1;
        collider.enabled = false;
        onTriggerEnter?.Invoke();

        if (!triggerOnce)
        {
            StartCoroutine(CanTriggerAgain());
        }
    }

    private IEnumerator CanTriggerAgain()
    {
        yield return new WaitForSeconds(10f);
        collider.enabled = true;
    }
}
