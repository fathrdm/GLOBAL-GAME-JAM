using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 2f; // Kecepatan gerak awan
    public float resetPositionX = 10f; // Posisi X untuk mereset awan
    public float startPositionX = -10f; // Posisi X awal setelah reset

    void Update()
    {
        // Gerakkan awan ke kiri
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Reset posisi awan jika melewati batas
        if (transform.position.x <= resetPositionX)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = startPositionX;
            transform.position = newPosition;
        }
    }
}
