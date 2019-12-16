using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Lock[] locks;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Lock>())
        {
            Lock fLock = collision.gameObject.GetComponent<Lock>();
            if (CheckLock(fLock))
            {
                if (fLock.isLocked == true)
                {
                    fLock.Unlock();
                }
            }
        }
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
