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
    public LineRenderer trajectoryLine; // LineRenderer untuk visualisasi trajectory
    public int trajectoryResolution = 50; // Resolusi trajectory

    private void Start()
    {
        mainCam = Camera.main;
        UpdateAvailableColors();

        // Pastikan LineRenderer diatur
        if (aimLine != null)
        {
            aimLine.positionCount = 2;
        }
        if (trajectoryLine != null)
        {
            trajectoryLine.positionCount = trajectoryResolution;
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
            ShowTrajectory();
        }
        else
        {
            ClearAimingAndTrajectory();
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
            ClearAimingAndTrajectory();
            UpdateAvailableColors();
        }
    }

    private void ShowAimingLine()
    {
        if (aimLine == null) return;

        aimLine.SetPosition(0, shootPoint.position);
        aimLine.SetPosition(1, mousePos);
        aimLine.enabled = true;
    }

    private void ShowTrajectory()
    {
        if (trajectoryLine == null || selectedPrefab == null) return;

        Vector3[] trajectoryPoints = CalculateTrajectoryPoints(shootPoint.position, transform.up * shootForce, trajectoryResolution);
        trajectoryLine.positionCount = trajectoryPoints.Length;
        trajectoryLine.SetPositions(trajectoryPoints);
        trajectoryLine.enabled = true;
    }

    private Vector3[] CalculateTrajectoryPoints(Vector3 startPosition, Vector3 velocity, int resolution)
    {
        Vector3[] points = new Vector3[resolution];
        points[0] = startPosition;

        float timeStep = 0.1f; // Waktu antar titik
        Vector3 gravity = Physics2D.gravity;

        for (int i = 1; i < resolution; i++)
        {
            float time = i * timeStep;
            points[i] = startPosition + velocity * time + 0.5f * gravity * time * time;

            // Periksa untuk memvisualisasikan pantulan
            RaycastHit2D hit = Physics2D.Raycast(points[i - 1], points[i] - points[i - 1], Vector3.Distance(points[i - 1], points[i]));
            if (hit.collider != null)
            {
                Vector3 reflectDir = Vector3.Reflect(points[i] - points[i - 1], hit.normal);
                velocity = reflectDir;
                startPosition = hit.point;
                i--; // Pastikan melanjutkan dari titik pantulan
            }
        }

        return points;
    }

    private void ClearAimingAndTrajectory()
    {
        if (aimLine != null)
        {
            aimLine.enabled = false;
        }
        if (trajectoryLine != null)
        {
            trajectoryLine.enabled = false;
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
                    rb.isKinematic = true;
                }
            }
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
