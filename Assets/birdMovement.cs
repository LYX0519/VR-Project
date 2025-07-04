using UnityEngine;

public class MiniGameBird : MonoBehaviour
{
    public float jumpAmount = 20f;
    public float gravity = -50f;
    public float minY = -30f;
    public float maxY = 30f;

    private Vector2 velocity;
    private RectTransform rect;

    public GameObject gameOverPanel;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpAmount;
        }

        // Apply gravity
        velocity.y += gravity * Time.unscaledDeltaTime;

        // Move bird
        Vector2 pos = rect.anchoredPosition;
        pos.y += velocity.y * Time.unscaledDeltaTime;

        // Clamp Y position
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        rect.anchoredPosition = pos;
    }
    
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }
}

