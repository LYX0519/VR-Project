using UnityEngine;
using TMPro;

public class MirrorKindness : MonoBehaviour
{
    public GameObject promptText;            // Text: "Press E to look in the mirror"
    public TextMeshProUGUI messageText;      // The wholesome feedback message
    public AudioClip chimeSound;             // Optional gentle chime sound

    private AudioSource audioSource;
    private bool playerInZone = false;

    // List of wholesome messages
    string[] wholesomeLines = new string[] {
        "You bring light into the room ",
        "You are enough, just as you are"
    };

    void Start()
    {
        // Hide text at the start
        if (promptText != null) promptText.SetActive(false);
        if (messageText != null) messageText.gameObject.SetActive(false);

        // Setup audio
        audioSource = gameObject.AddComponent<AudioSource>();
        if (chimeSound != null)
        {
            audioSource.clip = chimeSound;
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            // Show random wholesome message
            if (messageText != null)
            {
                 promptText.SetActive(false);
                string randomLine = wholesomeLines[Random.Range(0, wholesomeLines.Length)];
                messageText.text = randomLine;
                messageText.gameObject.SetActive(true);
            }

            // Play optional sound
            if (audioSource != null && chimeSound != null)
                audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            if (promptText != null) promptText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            if (promptText != null) promptText.SetActive(false);
            if (messageText != null) messageText.gameObject.SetActive(false);
        }
    }
}
