using UnityEngine;

public class BubbleDropManager : MonoBehaviour
{
    public GameObject parentObject; // Objek yang berisi semua bubble
    public float dropInterval = 15f; // Waktu antar penurunan dalam detik
    public float dropDistance = 1f; // Jarak penurunan per grid
    public float bottomLimit = -5f; // Batas bawah posisi bubble

    private float timer;

    private void Start()
    {
        if (parentObject == null)
        {
            Debug.LogError("Parent object is not assigned! Please assign the parent of all bubbles.");
            enabled = false; // Disable the script if no parent object is assigned
        }
        else
        {
            timer = dropInterval; // Initialize the timer
        }
    }

    private void Update()
    {
        if (parentObject == null) return;

        // Countdown timer
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = dropInterval; // Reset the timer
            DropBubbles();
        }
    }

    private void DropBubbles()
    {
        foreach (Transform bubble in parentObject.transform)
        {
            Vector3 newPosition = bubble.position + Vector3.down * dropDistance;

            // Check if the bubble has reached the bottom limit
            if (newPosition.y <= bottomLimit)
            {
                Debug.Log("Game Over! Bubbles reached the bottom.");
                GameOver();
                return;
            }

            // Apply the new position
            bubble.position = newPosition;
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0; // Pause the game
        Debug.Log("Game Over! You lost the game.");
        // Add Game Over logic here (e.g., show a Game Over panel)
    }
}
