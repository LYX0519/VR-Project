using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public Transform door; // assign the door GameObject in Inspector
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool playerInZone = false;
    private bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

 public AudioSource openSound;
    void Start()
    {
        initialRotation = door.rotation;
        targetRotation = Quaternion.Euler(door.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
             if (isOpen && openSound != null)
                openSound.Play();
        }

        if (isOpen)
            door.rotation = Quaternion.Slerp(door.rotation, targetRotation, Time.deltaTime * openSpeed);
        else
            door.rotation = Quaternion.Slerp(door.rotation, initialRotation, Time.deltaTime * openSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInZone = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInZone = false;
    }
}
