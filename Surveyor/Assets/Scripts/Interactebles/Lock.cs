using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Snappable
{
    private bool isLocked = true;
    [SerializeField] private Animator targetedDoor;
    [SerializeField] private AudioClip openDoorAudio;
    [SerializeField] private AudioSource audioSource;

    public override void OnSnap()
    {
        Unlock();
    }

    public void Unlock()
    {
        if(isLocked == true)
        {
            audioSource.PlayOneShot(openDoorAudio);
            targetedDoor.SetTrigger("OpenDoor");
            isLocked = false;
        }
    }
}
