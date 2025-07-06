using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetControl : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem waterStream;
    public Transform waterSurface; // Empty GameObject at sink base
    public float fillSpeed = 0.1f;
    public float maxWaterHeight = 0.5f; // Sink depth
    
    [Header("Overflow")]
    public GameObject overflowEffect; // Particle system for overflow
    public float overflowDelay = 5f; // Time until overflow if left on
    
    private bool isRunning = false;
    private float currentWaterHeight = 0f;
    private float faucetTimer = 0f;

    void Update()
    {
        // Toggle faucet on 'F' key press
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFaucet();
        }

        // Handle water filling
        if (isRunning)
        {
            // Increase water level
            if (currentWaterHeight < maxWaterHeight)
            {
                currentWaterHeight += fillSpeed * Time.deltaTime;
                waterSurface.localScale = new Vector3(1, currentWaterHeight, 1);
                waterSurface.position = new Vector3(
                    waterSurface.position.x,
                    waterSurface.position.y + (fillSpeed * Time.deltaTime)/2,
                    waterSurface.position.z
                );
            }
            // Overflow handling
            else
            {
                faucetTimer += Time.deltaTime;
                if (faucetTimer >= overflowDelay && !overflowEffect.activeSelf)
                {
                    overflowEffect.SetActive(true);
                }
            }
        }
    }

    void ToggleFaucet()
    {
        isRunning = !isRunning;
        
        if (isRunning)
        {
            waterStream.Play();
        }
        else
        {
            waterStream.Stop();
            overflowEffect.SetActive(false);
            faucetTimer = 0f;
        }
    }

    // Optional: Click to toggle instead of key press
    void OnMouseDown()
    {
        ToggleFaucet();
    }
}