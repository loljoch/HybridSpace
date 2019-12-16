using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem.Sample;

public class Key : MonoBehaviour
{
    public Lock[] locks;
    public LockToPoint lockTo;
    private Lock fLock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Lock>())
        {
            fLock = other.GetComponent<Lock>();
            if (CheckLock(fLock))
            {
                if (fLock.isLocked == true)
                {
                    fLock.isLocked = false;
                    lockTo.snapTo = fLock.keyLockPoint;
                }
            }
        }
    }

    public void OpenDoor()
    {
        fLock.Unlock();
    }

    private bool CheckLock(Lock _lock)
    {
        for (int i = 0; i < locks.Length; i++)
        {
            if (_lock == locks[i])
            {
                return true;
            }
        }

        return false;
    }
}
