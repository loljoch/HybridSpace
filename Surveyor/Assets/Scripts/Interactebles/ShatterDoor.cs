using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterDoor : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private MeshCollider[] shatteredPartsColl;
    private Rigidbody[] shatteredPartsRb;
    private BoxCollider ownCollider;

    private void Start()
    {
        ownCollider = GetComponent<BoxCollider>();
        shatteredPartsColl = GetComponentsInChildren<MeshCollider>();
        shatteredPartsRb = GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Boulder>())
        {
            audioSource.Play();
            Shatter();
        }
    }

    private void Shatter()
    {
        ownCollider.enabled = false;

        for (int i = 0; i < shatteredPartsColl.Length; i++)
        {
            shatteredPartsColl[i].enabled = true;
            shatteredPartsRb[i].useGravity = true;
        }
        GetComponent<Animator>().SetTrigger("Shatter");
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
