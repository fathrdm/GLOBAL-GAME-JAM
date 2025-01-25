using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tembak : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bubblePrefab; // Prefab bubble
    public Transform shootPoint; // Tempat keluar bubble
    public bool canFire = true;
    private float timer;
    public float timeBetweenFiring = 0.5f;
    public float shootForce = 10f; // Kecepatan bubble
    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {

        // Ambil posisi mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Hitung rotasi shooter mengikuti mouse
        Vector3 direction = mousePos - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotZ = Mathf.Clamp(rotZ, 0f, 180f); // Batasi rotasi agar hanya ke atas
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90f);

        // Timer untuk menunggu waktu antar tembakan
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        // Deteksi klik kiri untuk menembakkan bubble
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            ShootBubble();
        }
    }

    private void ShootBubble()
    {
        // Buat bubble baru
        GameObject bubble = Instantiate(bubblePrefab, shootPoint.position, Quaternion.identity);

        // Terapkan gaya agar bubble bergerak
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
        }
    }
}
