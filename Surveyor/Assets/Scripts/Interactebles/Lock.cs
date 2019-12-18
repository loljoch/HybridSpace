using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Snappable
{
    public bool isLocked;
    [SerializeField] private Animator targetedDoor;

    public override void OnSnap()
    {
        Unlock();
    }

    public void Unlock()
    {
        targetedDoor.SetTrigger("OpenDoor");
    }
}
