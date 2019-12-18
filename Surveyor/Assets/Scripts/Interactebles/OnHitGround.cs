using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitGround : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips; 
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayRandomSound();
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    private void PlayRandomSound()
    {
        int n = Random.Range(1, audioClips.Length);
        audioSource.clip = audioClips[n];
        audioSource.PlayOneShot(audioSource.clip);

        audioClips[n] = audioClips[0];
        audioClips[0] = audioSource.clip;
    }
}
