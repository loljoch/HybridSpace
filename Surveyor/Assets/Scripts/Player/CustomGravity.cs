using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    [SerializeField] private float gravityScale = 1f;
    private static float globalGravity = -9.81f;
    private Rigidbody rb;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        Vector3 gravity = Vector3.up * globalGravity * gravityScale;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
