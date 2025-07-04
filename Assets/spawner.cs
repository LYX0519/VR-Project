using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float minY = -20f;
    public float maxY = 20f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= spawnRate)
        {
            timer = 0f;
            SpawnPipe();
        }
    }

   void SpawnPipe()
{
    GameObject newPipe = Instantiate(pipePrefab, transform);

    float randomY = Random.Range(minY, maxY);

    // Move pipe into correct Y position
    newPipe.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, randomY);

    // ✅ Send this Y position as the center of the scoring gap
    newPipe.GetComponent<PipeMovement>().gapYCenter = randomY;
}

    
}
