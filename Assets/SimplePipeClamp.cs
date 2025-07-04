using UnityEngine;

public class SimplePipeClamp : MonoBehaviour
{
    public float speed = 40f;

    public float minY = -15f;
    public float maxY = 15f;

    public float leftBound = -80f;
    public float rightResetX = 80f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Move pipe left
        rect.anchoredPosition += Vector2.left * speed * Time.unscaledDeltaTime;

        // Reset pipe if it goes off-screen (left)
        if (rect.anchoredPosition.x < leftBound)
        {
            float randomY = Random.Range(minY, maxY);
            rect.anchoredPosition = new Vector2(rightResetX, randomY);
        }

        // Optional: Clamp vertical position
        Vector2 pos = rect.anchoredPosition;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        rect.anchoredPosition = pos;
    }
}
