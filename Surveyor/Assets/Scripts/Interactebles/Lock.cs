using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool isLocked;
    [SerializeField] private Animator targetedDoor;

    public void Unlock()
    {
        targetedDoor.SetTrigger("OpenDoor");
    }
}
