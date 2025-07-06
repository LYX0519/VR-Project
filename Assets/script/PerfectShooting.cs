using UnityEngine;

public class PerfectShooting : MonoBehaviour
{
    public Transform ballHoldPosition;
    public float shootForce = 15f;
    public float shootArc = 0.5f;
    private GameObject currentBall;
    private bool hasBall = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hasBall)
            {
                TryPickupBall();
            }
            else
            {
                ShootPerfectly();
            }
        }
    }

    void TryPickupBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Basketball") && !col.GetComponent<Basketball>().isHeld)
            {
                currentBall = col.gameObject;
                currentBall.GetComponent<Basketball>().PickUp(ballHoldPosition);
                hasBall = true;
                break;
            }
        }
    }

    void ShootPerfectly()
    {
        if (!hasBall || currentBall == null) return;

        // Find the nearest hoop
        BasketballHoop hoop = FindNearestHoop();
        if (hoop == null) return;

        // Calculate perfect shot trajectory
        Vector3 hoopCenter = hoop.transform.position + hoop.rimOffset;
        Vector3 toHoop = hoopCenter - ballHoldPosition.position;
        
        // Calculate required force using projectile motion equations
        float gravity = Physics.gravity.magnitude;
        float distance = new Vector2(toHoop.x, toHoop.z).magnitude;
        float height = toHoop.y;
        
        float angle = Mathf.Atan((distance * Mathf.Tan(shootArc) + height) / distance);
        float velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * angle));
        
        Vector3 direction = toHoop.normalized;
        direction.y = Mathf.Tan(angle); // Adjust vertical component
        
        currentBall.GetComponent<Basketball>().Shoot(direction * velocity);
        hasBall = false;
        currentBall = null;
    }

    BasketballHoop FindNearestHoop()
    {
        BasketballHoop nearest = null;
        float minDistance = Mathf.Infinity;
        
        foreach (BasketballHoop hoop in FindObjectsOfType<BasketballHoop>())
        {
            float distance = Vector3.Distance(transform.position, hoop.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = hoop;
            }
        }
        return nearest;
    }
}
