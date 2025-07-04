using UnityEngine;

public class ArcadeInteraction : MonoBehaviour
{
    public GameObject promptUI;        
    public GameObject miniGameCanvas; 
    public AudioSource startAudioSource; 

    private bool isPlayerNear = false;

    void Start()
    {
        miniGameCanvas.SetActive(false); 
       
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            miniGameCanvas.SetActive(true);  
            promptUI.SetActive(false);  
             startAudioSource.Play();     
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            promptUI.SetActive(false);
            miniGameCanvas.SetActive(false);
            startAudioSource.Stop(); 
        }
    }
}
