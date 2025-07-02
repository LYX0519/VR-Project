using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;
    public Transform modelTransform; // optional: for just rotating the visual

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Get camera directions
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Move direction
        Vector3 move = camForward * v + camRight * h;

        // Move the character (can move in any direction)
        transform.Translate(move * speed * Time.deltaTime, Space.World);

        // Only rotate the character if pressing forward (W or ↑)
        if (move.magnitude > 0.1f && v > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
