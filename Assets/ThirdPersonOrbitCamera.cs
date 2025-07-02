using UnityEngine;

public class ThirdPersonOrbitCamera : MonoBehaviour
{
    public Transform target;           // Character
    public float distance = 3.0f;      // Distance behind the character
    public float height = 1.5f;        // Height above the character
    public float mouseSensitivity = 3f;
    public float rotationSmoothTime = 0.1f;

    float yaw = 0f;
    float pitch = 15f; // Looking slightly downward

    void LateUpdate()
    {
        if (target == null) return;

        // Mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -10f, 45f); // limit up/down camera angle

        // Rotation around character
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // Final camera position
        Vector3 cameraPosition = target.position + Vector3.up * height + offset;
        transform.position = cameraPosition;
        transform.LookAt(target.position + Vector3.up * height);
    }
}

