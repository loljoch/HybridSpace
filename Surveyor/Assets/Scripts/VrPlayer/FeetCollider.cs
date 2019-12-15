using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public float feetHeight;

    public void SetFeet()
    {
        Vector3 curPos = playerTransform.position;
        curPos.x = Mathf.RoundToInt(curPos.x);
        curPos.z = Mathf.RoundToInt(curPos.z);
        curPos.y = feetHeight;
        playerTransform.position = curPos;
    }
}
