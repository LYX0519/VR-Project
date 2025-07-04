using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 50f;

    void Update()
    {
        transform.localPosition += Vector3.left * speed * Time.unscaledDeltaTime;

        if (transform.localPosition.x < -50f)
            Destroy(gameObject);
    }
}