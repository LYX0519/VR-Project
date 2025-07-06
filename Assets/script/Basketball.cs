using UnityEngine;

public class Basketball : MonoBehaviour
{
    public bool isHeld = false;
    private Rigidbody rb;
    public float minBounceVelocity = 2f; // Minimum velocity for bounce sound
    public AudioClip bounceSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Play bounce sound when hitting surfaces with enough velocity
        if (!isHeld && rb.velocity.magnitude > minBounceVelocity)
        {
            // Calculate volume based on impact strength
            float volume = Mathf.Clamp01(rb.velocity.magnitude / 10f);
            audioSource.PlayOneShot(bounceSound, volume);
            
            // Add random spin
            rb.AddTorque(new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            ));
        }
    }

    public void PickUp(Transform holder)
    {
        isHeld = true;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(holder);
        transform.localPosition = Vector3.zero + holder.forward * 0.5f;
    }

    public void Shoot(Vector3 force)
    {
        isHeld = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
