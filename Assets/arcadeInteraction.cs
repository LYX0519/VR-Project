using UnityEngine;

public class ArcadeInteraction : MonoBehaviour
{
    public GameObject promptUI;        // Text that says "Press E to play"
    public GameObject miniGameCanvas; // The whole mini-game group

    private bool isPlayerNear = false;

    void Start()
    {
        miniGameCanvas.SetActive(false); // Make sure it's off at start
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            miniGameCanvas.SetActive(true);  // Start the mini-game
            promptUI.SetActive(false);       // Hide prompt
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
        }
    }
}
