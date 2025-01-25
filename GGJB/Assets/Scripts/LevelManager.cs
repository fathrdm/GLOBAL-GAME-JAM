using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public Transform bubblesArea; // Area tempat semua bubble berada
    public Grid grid; // Grid hexagonal yang digunakan

    public List<GameObject> bubblesInScene;
    public List<string> colorsInScene;
    public List<GameObject> bubblesPrefabs;
    public List<GameObject> levels;
    public int currentLevel = 0;
    private static readonly Vector3Int[] hexNeighborOffsets = new Vector3Int[]
    {
        new Vector3Int(1, 0, 0),  // Kanan
        new Vector3Int(-1, 0, 0), // Kiri
        new Vector3Int(0, 1, 0),  // Atas kanan
        new Vector3Int(0, -1, 0), // Bawah kiri
        new Vector3Int(1, -1, 0), // Bawah kanan
        new Vector3Int(-1, 1, 0)  // Atas kiri
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    System.Collections.IEnumerator LoadLevel(int level)
    {
        yield return new WaitForSeconds(0.1f);

        //ScoreManager.GetInstance().Reset();
        GameObject levelToLoad = Instantiate(levels[level]);
        FillWithBubbles(levelToLoad, bubblesPrefabs);

        SnapChildrensToGrid(bubblesArea);
        UpdateListOfBubblesInScene();

        GameManager.instance.shootScript.CreateNewBubbles();
    }

    /// <summary>
    /// Menambahkan bubble sebagai anak dari area bubble dan memeriksa bubble yang terhubung.
    /// </summary>
    public void SetAsBubbleAreaChild(Transform bubble)
    {
        SnapToNearestGridPosition(bubble);
        bubble.SetParent(bubblesArea);

        // Periksa dan hancurkan bubble yang terhubung
        CheckAndDestroyMatchingBubbles(bubble);
    }

    /// <summary>
    /// Menghancurkan semua bubble yang terdapat di dalam daftar.
    /// </summary>
    public void DestroyBubbles(List<Transform> bubbles)
    {
        foreach (Transform bubble in bubbles)
        {
            Destroy(bubble.gameObject); // Hapus bubble dari scene
        }
    }

    /// <summary>
    /// Memastikan posisi bubble sesuai dengan posisi grid hexagonal terdekat.
    /// </summary>
    private void SnapToNearestGridPosition(Transform bubble)
    {
        Vector3Int cellPosition = grid.WorldToCell(bubble.position);
        bubble.position = grid.GetCellCenterWorld(cellPosition);
        bubble.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Mendapatkan tetangga hexagonal dari bubble tertentu.
    /// </summary>
    private List<Transform> GetHexagonalNeighbors(Transform bubble)
    {
        List<Transform> neighbors = new List<Transform>();
        Vector3Int cellPosition = grid.WorldToCell(bubble.position);

        foreach (Vector3Int offset in hexNeighborOffsets)
        {
            Vector3Int neighborCell = cellPosition + offset;
            Vector3 neighborWorldPos = grid.GetCellCenterWorld(neighborCell);

            // Cek apakah ada bubble di posisi neighbor
            foreach (Transform child in bubblesArea)
            {
                if (Vector3.Distance(child.position, neighborWorldPos) < 0.1f) // Toleransi jarak
                {
                    neighbors.Add(child);
                }
            }
        }

        return neighbors;
    }

    /// <summary>
    /// Memeriksa dan menghancurkan bubble yang memiliki warna sama dan saling terhubung.
    /// </summary>
    public void CheckAndDestroyMatchingBubbles(Transform bubble)
    {
        List<Transform> toDestroy = new List<Transform>();
        Queue<Transform> queue = new Queue<Transform>();
        HashSet<Transform> visited = new HashSet<Transform>();

        // Mulai dari bubble yang baru ditembak
        queue.Enqueue(bubble);
        visited.Add(bubble);

        while (queue.Count > 0)
        {
            Transform current = queue.Dequeue();
            toDestroy.Add(current);

            // Cek tetangga dengan warna yang sama
            foreach (Transform neighbor in GetHexagonalNeighbors(current))
            {
                Bubble neighborScript = neighbor.GetComponent<Bubble>();
                Bubble currentScript = current.GetComponent<Bubble>();

                if (neighborScript != null && currentScript != null &&
                    neighborScript.bubbleColor == currentScript.bubbleColor &&
                    !visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    public int GetBubbleAreaChildCount()
    {
        return bubblesArea.childCount;
    }

    public void UpdateListOfBubblesInScene()
    {
        List<string> colors = new List<string>();
        List<GameObject> newListOfBubbles = new List<GameObject>();

        foreach (Transform t in bubblesArea)
        {
            Bubble bubbleScript = t.GetComponent<Bubble>();
            if (colors.Count < bubblesPrefabs.Count && !colors.Contains(bubbleScript.bubbleColor.ToString()))
            {
                string color = bubbleScript.bubbleColor.ToString();

                foreach (GameObject prefab in bubblesPrefabs)
                {
                    if (color.Equals(prefab.GetComponent<Bubble>().bubbleColor.ToString()))
                    {
                        colors.Add(color);
                        newListOfBubbles.Add(prefab);
                    }
                }
            }
        }

        colorsInScene = colors;
        bubblesInScene = newListOfBubbles;
    }

    private void SnapChildrensToGrid(Transform parent)
    {
        foreach (Transform t in parent)
        {
            SnapToNearestGripPosition(t);
        }
    }

    public void SnapToNearestGripPosition(Transform t)
    {
        Vector3Int cellPosition = grid.WorldToCell(t.position);
        t.position = grid.GetCellCenterWorld(cellPosition);
        t.rotation = Quaternion.identity;

    }

    private void FillWithBubbles(GameObject go, List<GameObject> _prefabs)
    {
        foreach (Transform t in go.transform)
        {
            var bubble = Instantiate(_prefabs[UnityEngine.Random.Range(0, _prefabs.Count)], bubblesArea);
            bubble.transform.position = t.position;
        }

        Destroy(go);
    }

    public void StartLevel(int level)
    {
        level = 0;
        //GameManager.instance.levelsUI.SetActive(false);
        if (level >= levels.Count)
            level = 0;

        currentLevel = level;
        //levelText.GetComponent<UnityEngine.UI.Text>().text = "Level " + (level + 1);
        StartCoroutine(LoadLevel(level));
    }


}