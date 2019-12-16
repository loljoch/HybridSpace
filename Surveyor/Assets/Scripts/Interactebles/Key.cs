using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem.Sample;

public class Key : MonoBehaviour
{
    public Snappable[] locks;
    public LockToPoint lockTo;
    private Snappable fSnappable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Snappable>())
        {
            fSnappable = other.GetComponent<Snappable>();
            if (CheckSnappable(fSnappable))
            {
                GetComponent<Collider>().enabled = false;
                lockTo.snapTo = fSnappable.keyLockPoint;
                lockTo.OnSnap.AddListener(fSnappable.OnSnap);
            }
        }
    }

    private bool CheckSnappable(Snappable _snappable)
    {
        for (int i = 0; i < locks.Length; i++)
        {
            if (_snappable == locks[i])
            {
                return true;
            }
        }

        return false;
    }
}
