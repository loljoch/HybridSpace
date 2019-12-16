using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltManager : MonoBehaviour
{

    [SerializeField] Transform cameraPivot;
    [SerializeField] private string LRaxis, DUaxis;
    [SerializeField] private List<Rigidbody> rbList;
    private Vector3 wantedVector3 = Vector3.zero;
    private float zAngle = 10;
    private float xAngle = 10;

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
        wantedVector3.x = xAngle * -Input.GetAxis(DUaxis);
        wantedVector3.z = zAngle * -Input.GetAxis(LRaxis);

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
