using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class PlayTVVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;            
    public VideoClip[] videoClips;              
    public TextMeshProUGUI floatingText;         

    private int currentChannel = 0;
    private bool playerInZone = false;

    void Start()
    {
        if (floatingText != null)
            floatingText.gameObject.SetActive(false);

        if (videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentChannel];
        }
    }

    void Update()
    {
        if (!playerInZone) return;

        // Play or pause video
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                UpdateFloatingText("Paused | Press E to Play | R to Change Channel");
            }
            else
            {
                videoPlayer.Play();
                UpdateFloatingText($"Playing Channel {currentChannel + 1} | Press E to Pause | R to Change");
            }
        }

        // Change channel
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchChannel();
        }
    }

   void SwitchChannel()
{
    if (videoClips.Length == 0) return;

    currentChannel = (currentChannel + 1) % videoClips.Length;

    videoPlayer.Stop(); // Stop the current video first
    videoPlayer.clip = videoClips[currentChannel]; // Change to new video
    videoPlayer.Prepare(); // Prepare the video before playing

    videoPlayer.prepareCompleted += OnVideoPrepared;
}

    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
        UpdateFloatingText($"Switched to Channel {currentChannel + 1} | Press E to Pause | R to Change");
        vp.prepareCompleted -= OnVideoPrepared;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            UpdateFloatingText($"Press E to Play | R to Change Channel\nChannel {currentChannel + 1}");
            if (floatingText != null)
                floatingText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            videoPlayer.Stop();
            if (floatingText != null)
                floatingText.gameObject.SetActive(false);
        }
    }

    void UpdateFloatingText(string text)
    {
        if (floatingText != null)
            floatingText.text = text;
    }
}
