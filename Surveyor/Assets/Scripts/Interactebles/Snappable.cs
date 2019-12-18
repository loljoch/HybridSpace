using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Snappable : MonoBehaviour
{
    public Transform keyLockPoint;

    public abstract void OnSnap();
}
