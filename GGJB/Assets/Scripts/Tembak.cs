using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tembak : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public List<GameObject> bubblePrefabs;
    public Transform shootPoint;
    public bool canFire = true;
    private float timer;
    public float timeBetweenFiring = 0.5f;
    public float shootForce = 10f;

    private List<Color> availableColors = new List<Color>();
    private GameObject previewBubble;
    private GameObject selectedPrefab;

    [Header("Aiming Line")]
    public LineRenderer aimLine; // LineRenderer untuk aiming
    public int lineSortingOrder = -1; // Sorting order untuk memastikan garis di bawah bubble

    private void Start()
    {
        mainCam = Camera.main;
        UpdateAvailableColors();

        if (aimLine != null)
        {
            aimLine.positionCount = 2;
            aimLine.sortingOrder = lineSortingOrder; // Atur sorting order untuk layer
        }
    }

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotZ = Mathf.Clamp(rotZ, 0f, 180f);
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90f);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0))
        {
            ShowAimingLine();
            UpdatePreviewBubblePosition();
        }
        else
        {
            ClearAimingLine();
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            ShowPreviewBubble();
        }

        if (Input.GetMouseButtonUp(0) && canFire)
        {
            canFire = false;
            ShootBubble();
            Destroy(previewBubble);
            ClearAimingLine();
            UpdateAvailableColors();
        }
    }

    private void ShowAimingLine()
    {
        if (aimLine == null || previewBubble == null) return;

        // Atur posisi garis
        aimLine.SetPosition(0, shootPoint.position);
        aimLine.SetPosition(1, mousePos);

        // Sinkronkan warna garis dengan warna SpriteRenderer preview bubble
        SpriteRenderer previewRenderer = previewBubble.GetComponent<SpriteRenderer>();
        if (previewRenderer != null)
        {
            Color previewColor = previewRenderer.color;
            aimLine.startColor = previewColor; // Warna awal garis
            aimLine.endColor = previewColor;   // Warna akhir garis
        }
        else
        {
            // Fallback warna garis jika tidak ada SpriteRenderer
            aimLine.startColor = Color.white;
            aimLine.endColor = Color.white;
        }

        aimLine.enabled = true;
    }


    private void ClearAimingLine()
    {
        if (aimLine != null)
        {
            aimLine.enabled = false;
        }
    }

    private void ShowPreviewBubble()
    {
        if (availableColors.Count > 0)
        {
            Color randomColor = availableColors[Random.Range(0, availableColors.Count)];
            selectedPrefab = GetPrefabByColor(randomColor);

            if (selectedPrefab != null)
            {
                previewBubble = Instantiate(selectedPrefab, shootPoint.position, Quaternion.identity);
                Rigidbody2D rb = previewBubble.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Pastikan preview bubble tidak bergerak
                }
            }
        }
    }

    private void UpdatePreviewBubblePosition()
    {
        if (previewBubble != null)
        {
            previewBubble.transform.position = shootPoint.position; // Ikuti posisi shootPoint
        }
    }

    private void ShootBubble()
    {
        if (selectedPrefab != null)
        {
            GameObject bubble = Instantiate(selectedPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
            }
        }
    }

    private GameObject GetPrefabByColor(Color color)
    {
        foreach (GameObject prefab in bubblePrefabs)
        {
            SpriteRenderer renderer = prefab.GetComponent<SpriteRenderer>();
            if (renderer != null && renderer.color == color)
            {
                return prefab;
            }
        }
        return null;
    }

    private void UpdateAvailableColors()
    {
        availableColors.Clear();
        SpriteRenderer[] allRenderers = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer renderer in allRenderers)
        {
            if (!availableColors.Contains(renderer.color))
            {
                availableColors.Add(renderer.color);
            }
        }
    }
}
