using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan pemain bergerak
    public float boundaryX = 7.5f; // Batas horizontal (agar pemain tidak keluar layar)

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Ambil input horizontal (A/D atau Left/Right Arrow)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Hitung posisi baru
        Vector2 newPosition = rb.position + Vector2.right * horizontalInput * moveSpeed * Time.deltaTime;

        // Batasi posisi agar tidak keluar layar
        newPosition.x = Mathf.Clamp(newPosition.x, -boundaryX, boundaryX);

        // Terapkan posisi baru
        rb.MovePosition(newPosition);
    }
}
