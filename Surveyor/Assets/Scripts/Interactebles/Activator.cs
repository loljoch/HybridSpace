using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public bool triggerOnce = true;
    private BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
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
