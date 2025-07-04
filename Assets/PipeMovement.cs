using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float speed = 50f;
    public float gapYCenter;           // Set when spawning
    public float scoreRange = 15f;     // Allowed Y range for scoring

    private bool scored = false;
    private Transform bird;

    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Move pipe left
        transform.localPosition += Vector3.left * speed * Time.unscaledDeltaTime;

        // Get bird's vertical Y position
        float birdY = bird.localPosition.y;

        // Check: pipe passed + bird is within the vertical gap
        if (!scored &&
            transform.localPosition.x < bird.localPosition.x &&
            Mathf.Abs(birdY - gapYCenter) < scoreRange)
        {
            scored = true;
            FindObjectOfType<ScoreManager>().AddPoint();
        }

        // Destroy off-screen
        if (transform.localPosition.x < -50f)
            Destroy(gameObject);
    }
}
