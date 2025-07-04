using UnityEngine;
using TMPro;

public class DrumInteraction : MonoBehaviour
{
    public AudioClip drumSound;
    private AudioSource audioSource;
    private bool playerInZone = false;

    public GameObject promptText; 
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = drumSound;

        if (promptText != null)
            promptText.SetActive(false);
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            if (promptText != null)
                promptText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        playerInZone = false;
        if (promptText != null)
            promptText.SetActive(false);

        if (audioSource.isPlaying)
            audioSource.Stop(); // 🛑 Stop the drum when leaving
    }
}


}
