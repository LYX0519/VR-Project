using UnityEngine;

public class SlidingDoorController : MonoBehaviour
{
    [Header("Door Movement Settings")]
    [Tooltip("How far the door slides open (in meters)")]
    public float slideDistance = 1.5f; // Easily adjustable in Inspector
    
    [Tooltip("How fast the door slides")]
    [Range(0.1f, 5f)]
    public float slideSpeed = 2f;
    
    [Header("Interaction Settings")]
    [Tooltip("How close player needs to be to interact")]
    public float interactionDistance = 2f;
    
    [Tooltip("Key to press to open/close")]
    public KeyCode interactKey = KeyCode.E;

    // Private variables
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool isMoving = false;
    private Transform player;

    void Start()
    {
        // Store initial position as closed position
        closedPosition = transform.position;
        
        // Calculate open position based on slide distance
        // Note: Using transform.right makes it slide to its local right
        // Change to transform.forward if you want different slide direction
        openPosition = closedPosition + transform.right * slideDistance;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // Only allow interaction when close and not currently moving
        if (distanceToPlayer <= interactionDistance && !isMoving)
        {
            // Check for key press
            if (Input.GetKeyDown(interactKey))
            {
                ToggleDoor();
            }
        }
        
        // Handle door movement
        if (isMoving)
        {
            MoveDoor();
        }
    }

    void ToggleDoor()
    {
        // Switch state
        isOpen = !isOpen;
        isMoving = true;
    }

    void MoveDoor()
    {
        // Determine target position based on current state
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        
        // Move towards target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * slideSpeed);
        
        // Check if we've reached the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }

    // Visual feedback for interaction
    void OnGUI()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= interactionDistance && !isMoving)
        {
            string message = isOpen ? "Press E to close" : "Press E to open";
            GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 200, 30), message);
        }
    }

    // Visualize the slide distance in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.right; // Change to forward if needed
        Gizmos.DrawLine(transform.position, transform.position + direction * slideDistance);
        Gizmos.DrawWireCube(transform.position + direction * slideDistance, Vector3.one * 0.2f);
    }
}