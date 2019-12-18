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
        if(!triggered)
        {
            triggered = true;
            onTriggerEnter?.Invoke();

            if (!triggerOnce)
            {
                StartCoroutine(CanTriggerAgain());
            }else{
                GetComponent<BoxCollider>().enabled = false;
            }
        }
        
    }

    private IEnumerator CanTriggerAgain()
    {
        yield return new WaitForSeconds(2f);
        triggered = false;
    }
}
