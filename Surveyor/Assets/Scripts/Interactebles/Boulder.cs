using UnityEngine;

public class Boulder : MonoBehaviour
{
    public float soundThreshold = 0.5f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private AudioClip rollingAudio, onImpact;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate() 
    {
        if(rb.velocity.x + rb.velocity.z > soundThreshold)
        {
            audioSource.clip = rollingAudio;
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == wallLayer)
        {
            audioSource.PlayOneShot(onImpact);
        }
    }
}
