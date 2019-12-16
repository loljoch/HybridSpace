using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TiltValve : Snappable
{
    public override void OnSnap()
    {
        GameObject valveObject = GetComponentInChildren<CircularDrive>().gameObject;
        valveObject.GetComponent<CircularDrive>().enabled = true;
        valveObject.GetComponent<Throwable>().enabled = false;
    }
}
