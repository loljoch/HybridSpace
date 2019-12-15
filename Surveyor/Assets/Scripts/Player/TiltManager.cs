using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltManager : MonoBehaviour
{
    [SerializeField] Transform cameraPivot;
    [SerializeField] private string LRaxis, DUaxis;
    [SerializeField] private List<Rigidbody> rbList;
    private Vector3 wantedVelocity = Vector3.zero;
    private float zAngle = 10;
    private float xAngle = 10;

    private void FixedUpdate()
    {
        wantedVelocity.x = Input.GetAxis(LRaxis);
        wantedVelocity.z = Input.GetAxis(DUaxis);
        TiltObjects();
        TiltCamera();
    }

    private void TiltCamera()
    {
        Vector3 pivotRotation = cameraPivot.localEulerAngles;
        pivotRotation.x = xAngle * Input.GetAxis(DUaxis);
        pivotRotation.z = zAngle * Input.GetAxis(LRaxis);
        cameraPivot.localEulerAngles = pivotRotation;
    }

    private void TiltObjects()
    {
        for (int i = 0; i < rbList.Count; i++)
        {
            rbList[i].velocity = wantedVelocity * rbList[i].mass;
        }
    }
}
