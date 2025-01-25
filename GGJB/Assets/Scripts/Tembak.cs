using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tembak : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public List<GameObject> bubblePrefabs; // Daftar prefab bubble dengan warna berbeda
    public Transform shootPoint; // Tempat keluar bubble
    public bool canFire = true;
    private float timer;
    public float timeBetweenFiring = 0.5f;
    public float shootForce = 10f; // Kecepatan bubble

    // Daftar warna yang tersedia di stage
    private List<Color> availableColors = new List<Color>();
    private GameObject previewBubble; // Bubble yang ditampilkan sebelum ditembak
    private GameObject selectedPrefab; // Prefab yang dipilih untuk ditembakkan

    private void Start()
    {
        mainCam = Camera.main;
        UpdateAvailableColors(); // Perbarui daftar warna saat permainan dimulai
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

        // Tampilkan preview bubble saat menahan klik kiri
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            ShowPreviewBubble();
        }

        // Luncurkan bubble saat melepas klik kiri
        if (Input.GetMouseButtonUp(0) && canFire)
        {
            canFire = false;
            ShootBubble();
            Destroy(previewBubble); // Hapus bubble preview
            UpdateAvailableColors(); // Perbarui daftar warna setelah menembak
        }
    }

    private void ShowPreviewBubble()
    {
        if (availableColors.Count > 0)
        {
            // Pilih warna secara acak dari daftar
            Color randomColor = availableColors[Random.Range(0, availableColors.Count)];

            // Cari prefab yang cocok dengan warna tersebut
            selectedPrefab = GetPrefabByColor(randomColor);

            if (selectedPrefab != null)
            {
                // Buat preview bubble dari prefab yang dipilih
                previewBubble = Instantiate(selectedPrefab, shootPoint.position, Quaternion.identity);

                // Matikan rigidbody pada preview agar tidak bergerak
                Rigidbody2D rb = previewBubble.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

    private void ShootBubble()
    {
        if (selectedPrefab != null)
        {
            // Buat bubble baru dari prefab yang dipilih
            GameObject bubble = Instantiate(selectedPrefab, shootPoint.position, Quaternion.identity);

            // Terapkan gaya agar bubble bergerak
            Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
            }
        }
    }

    private GameObject GetPrefabByColor(Color color)
    {
        // Cari prefab yang warnanya sesuai dengan warna yang diberikan
        foreach (GameObject prefab in bubblePrefabs)
        {
            SpriteRenderer renderer = prefab.GetComponent<SpriteRenderer>();
            if (renderer != null && renderer.color == color)
            {
                return prefab;
            }
        }
        return null; // Jika tidak ada prefab yang cocok
    }

    private void UpdateAvailableColors()
    {
        // Bersihkan daftar warna yang tersedia
        availableColors.Clear();

        // Temukan semua objek di stage dengan komponen SpriteRenderer
        SpriteRenderer[] allRenderers = FindObjectsOfType<SpriteRenderer>();

        // Tambahkan warna unik dari objek ke daftar availableColors
        foreach (SpriteRenderer renderer in allRenderers)
        {
            if (!availableColors.Contains(renderer.color))
            {
                availableColors.Add(renderer.color);
            }
        }
    }
}
