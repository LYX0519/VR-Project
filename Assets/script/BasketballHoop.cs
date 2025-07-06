using UnityEngine;

public class BasketballHoop : MonoBehaviour
{
    public Vector3 rimOffset = new Vector3(0, 0.3f, 0); // Adjust to match your hoop center
    public float rimRadius = 0.45f; // Standard NBA rim size
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + rimOffset, rimRadius);
    }
}