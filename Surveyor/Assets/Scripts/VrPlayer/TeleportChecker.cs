using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportChecker : MonoBehaviour
{
    public UnityEvent onTeleport;
    [SerializeField] private Transform playerTransform;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = playerTransform.position;
    }

    void Update()
    {
        if (lastPosition != playerTransform.position)
        {
            onTeleport?.Invoke();
        }
    }

    //private void OnTeleport()
    //{

    //}
}
