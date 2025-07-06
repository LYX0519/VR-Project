using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public Transform cameraTransform;
    public Transform modelTransform;

    [Header("Basketball Controls")]
    public Transform ballHoldPosition;
    public float pickupRange = 2f;
    public float shootForce = 10f;
    public float shootArc = 0.5f;
    public LayerMask ballLayerMask; // Create a layer for basketballs
    
    private GameObject currentBall;
    private bool hasBall = false;
    private CharacterController controller;
    private bool showPickupPrompt = false;
    private bool showShootPrompt = false;

    [Header("Aim Assistance")]
public float aimAssistAngle = 15f; // Degrees of assistance
public float maxAssistDistance = 10f; // How far to check for hoops

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Safety checks
        if (ballHoldPosition == null)
        {
            Debug.LogError("Ball hold position not assigned!");
            ballHoldPosition = new GameObject("BallHoldPos").transform;
            ballHoldPosition.SetParent(transform);
            ballHoldPosition.localPosition = new Vector3(0, 0, 0.5f);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleBasketballInput();
        CheckForBasketballs();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * v + camRight * h;
        controller.Move(move * speed * Time.deltaTime);

        if (move.magnitude > 0.1f && v > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void CheckForBasketballs()
    {
        showPickupPrompt = false;
        
        if (!hasBall)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange, ballLayerMask);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Basketball") && col.TryGetComponent<Basketball>(out var ball) && !ball.isHeld)
                {
                    showPickupPrompt = true;
                    break;
                }
            }
        }
        
        showShootPrompt = hasBall;
    }

    void HandleBasketballInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hasBall && showPickupPrompt)
            {
                TryPickupBall();
            }
            else if (hasBall)
            {
                ShootBall();
            }
        }
    }

    void TryPickupBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange, ballLayerMask);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Basketball") && col.TryGetComponent<Basketball>(out var ball) && !ball.isHeld)
            {
                currentBall = col.gameObject;
                ball.PickUp(ballHoldPosition);
                hasBall = true;
                showPickupPrompt = false;
                showShootPrompt = true;
                break;
            }
        }
    }

void ShootBall()
{
    if (!hasBall || currentBall == null) return;

    // Get basic shoot direction
    Vector3 shootDirection = cameraTransform.forward + (Vector3.up * shootArc);
    
    // Find nearest basketball hoop
    BasketballHoop nearestHoop = FindNearestHoop();
    
    if (nearestHoop != null)
    {
        // Calculate vector to hoop center
        Vector3 hoopCenter = nearestHoop.transform.position + nearestHoop.rimOffset;
        Vector3 toHoop = (hoopCenter - ballHoldPosition.position).normalized;
        
        // Blend between player aim and perfect shot
        shootDirection = Vector3.Slerp(shootDirection, toHoop, 
                                    Mathf.Clamp01(1 - (Vector3.Angle(shootDirection, toHoop) / aimAssistAngle)));
    }
    
    currentBall.GetComponent<Basketball>().Shoot(shootDirection * shootForce);
    hasBall = false;
    currentBall = null;
}

BasketballHoop FindNearestHoop()
{
    BasketballHoop nearest = null;
    float minDistance = float.MaxValue;
    
    foreach (BasketballHoop hoop in FindObjectsOfType<BasketballHoop>())
    {
        float distance = Vector3.Distance(transform.position, hoop.transform.position);
        if (distance < maxAssistDistance && distance < minDistance)
        {
            minDistance = distance;
            nearest = hoop;
        }
    }
    return nearest;
}

    void OnGUI()
    {
        if (showPickupPrompt)
        {
            GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 200, 30), "Press Spacebar to pick up ball");
        }
        else if (showShootPrompt)
        {
            GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 200, 30), "Press Spacebar to shoot");
        }
    }
}