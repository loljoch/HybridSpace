using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField] private AudioClip rollingAudio, onImpact;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate() 
    {
        if(rb.velocity.x + rb.velocity.z > 0)
        {
            audioSource.clip = rollingAudio;
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(rb.velocity.x + rb.velocity.z > 0)
        {
            audioSource.PlayOneShot(onImpact);
        }
    }
}
