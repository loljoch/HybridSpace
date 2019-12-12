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
    }

    private void TiltCamera()
    {

    }

    private void TiltObjects()
    {
        for (int i = 0; i < rbList.Count; i++)
        {
            rbList[i].velocity = wantedVelocity * rbList[i].mass;
        }
    }
}
