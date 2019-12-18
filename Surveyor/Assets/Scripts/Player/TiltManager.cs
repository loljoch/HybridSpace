using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TiltManager : MonoBehaviour
{
    private int zDirection, xDirection;
    public int XDirection {get{return xDirection;} set{xDirection = value;}}
    public int ZDirection {get{return zDirection;} set{zDirection = value;}}
    [SerializeField] Transform cameraPivot;
    [SerializeField] private string LRaxis, DUaxis;
    [SerializeField] private List<Rigidbody> rbList;
    private Vector3 wantedVector3 = Vector3.zero;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;


    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(cameraPivot.localEulerAngles, wantedVector3);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) && xDirection == -1)
        {
            //tiltXleft
            Debug.Log("TiltedLeft");
        }

        if(Input.GetKey(KeyCode.RightArrow) && xDirection == 1)
        {
            //tiltXright
            Debug.Log("TiltedRight");

        }

        if(Input.GetKey(KeyCode.UpArrow) && zDirection == -1)
        {
            //tiltZforwarddown
            Debug.Log("TiltedForwardDown");

        }

        if(Input.GetKey(KeyCode.DownArrow) && zDirection == 1)
        {
            //tiltZforwardup
            Debug.Log("TiltedForwardUp");

        }
    }

    private void FixedUpdate()
    {
        TiltObjects();
    }

    private void TiltObjects()
    {
        Vector3 tiltVelocity = Vector3.zero;
        tiltVelocity.x = -wantedVector3.z;
        tiltVelocity.z = -wantedVector3.x;

        for (int i = 0; i < rbList.Count; i++)
        {
            rbList[i].velocity = tiltVelocity * rbList[i].mass;
        }
    }

}
