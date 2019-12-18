using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TiltManager : MonoBehaviour
{
    public CircularDrive zValve, xValve;
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
        float xAngleMax = xValve.outAngle / 4.5f;
        float zAngleMax = zValve.outAngle / 4.5f;
        wantedVector3.x = xAngleMax * -Input.GetAxis(DUaxis);
        wantedVector3.z = zAngleMax * -Input.GetAxis(LRaxis);

        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        cameraPivot.localEulerAngles = Vector3.Lerp(cameraPivot.localEulerAngles, wantedVector3, fractionOfJourney);
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
