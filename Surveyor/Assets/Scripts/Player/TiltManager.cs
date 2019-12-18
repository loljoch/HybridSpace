using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TiltManager : MonoBehaviour
{
    private int zDirection, xDirection;
    public int XDirection {get{return xDirection;} set{xDirection = value;}}
    public int ZDirection {get{return zDirection;} set{zDirection = value;}}
    [SerializeField] Animator tiltAnimator;
    [SerializeField] private string LRaxis, DUaxis;
    [SerializeField] private List<Rigidbody> rbList;
    [SerializeField] private AudioClip tiltAudio;
    [SerializeField] private AudioSource audioSource;
    private Vector3 wantedVector3 = Vector3.zero;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        tiltAnimator.SetInteger("zDirection", 0);
        tiltAnimator.SetInteger("xDirection", 0);
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && xDirection == -1)
        {
            tiltAnimator.SetInteger("xDirection", xDirection);
            audioSource.PlayOneShot(tiltAudio);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) && xDirection == 1)
        {
            tiltAnimator.SetInteger("xDirection", xDirection);
            audioSource.PlayOneShot(tiltAudio);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && zDirection == -1)
        {
            tiltAnimator.SetInteger("zDirection", zDirection);
            audioSource.PlayOneShot(tiltAudio);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) && zDirection == 1)
        {
            tiltAnimator.SetInteger("zDirection", zDirection);
            audioSource.PlayOneShot(tiltAudio);
        }

        if(Input.GetKeyDown(KeyCode.Space) && tiltAnimator.GetInteger("zDirection") + tiltAnimator.GetInteger("xDirection") != 0)
        {
            tiltAnimator.SetInteger("zDirection", 0);
            tiltAnimator.SetInteger("xDirection", 0);
            audioSource.PlayOneShot(tiltAudio);
        }

        wantedVector3.x = tiltAnimator.GetInteger("xDirection");
        wantedVector3.z = tiltAnimator.GetInteger("zDirection");
    }

    private void FixedUpdate()
    {
        TiltObjects();
    }

    private void TiltObjects()
    {
        Vector3 tiltVelocity = Vector3.zero;
        tiltVelocity.x = wantedVector3.z;
        tiltVelocity.z = -wantedVector3.x;

        for (int i = 0; i < rbList.Count; i++)
        {
            rbList[i].velocity = tiltVelocity * rbList[i].mass;
        }
    }

}
