using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton untuk akses global
    public GameObject parentObject;

    [Header("Game Setting")]
    public bool isOver;
    public bool isPause;

    [Header("Panels")]
    [SerializeField] GameObject PausePanel;
    public GameObject GameOverPanel;
    [SerializeField] GameObject settingsMenuPanel;
    [SerializeField] GameObject continuePanel;

    [Header("Game Over Settings")]
    public Transform gameOverLimit; // Transform yang menentukan batas kekalahan

    public string gameState = "play";
    //public Shooter shootScript;
    private const int SEQUENCE_SIZE = 3; // Jumlah bubble minimum untuk meledak

    private List<Transform> sequenceBubbles; // Menyimpan bubble dalam sequence
    private List<Transform> bubblesToDrop = new List<Transform>(); // Menyimpan bubble yang akan jatuh

    private List<Transform> activeBubbles = new List<Transform>(); // Daftar bubble yang ada di permainan
    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        sequenceBubbles = new List<Transform>();
    }

    private void Start()
    {
        Time.timeScale = 1;
        // Buat panel
        PausePanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        continuePanel.SetActive(false);
        GameOverPanel.SetActive(false); 
        isPause = false;
        isOver = false;

        if (parentObject == null)
        {
            Debug.LogWarning("Parent object is not assigned!");
            return;
        }

        continuePanel.SetActive(false); // Ensure the panel is initially hidden
        
    }

    private void Update()
    {
        GamePause();
        CheckGameOverCondition(); // Periksa kondisi kekalahan setiap frame
                                  //CheckWinCondition(); // Periksa kondisi kemenangan setiap frame
        if (parentObject == null) return;

        int childCount = parentObject.transform.childCount;

        CheckWinCondition(childCount);
    }

   

    public void RegisterBubble(Transform bubble)
    {
        activeBubbles.Add(bubble);
    }

    public void UnregisterBubble(Transform bubble)
    {
        activeBubbles.Remove(bubble);
    }

    public void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;

            if (isPause)
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PausePanel.SetActive(false);
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void GameResume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void BackButton()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
    }

    public void Settings()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void continuee()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(false);
        continuePanel.SetActive(true);
    }

    public void ContinueUnlockLevel()
    {
        SceneManager.LoadScene("Level 2");

        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    public void ProcessTurn(Transform currentBubble)
    {
        StartCoroutine(CheckSequence(currentBubble));
        StartCoroutine(ProcessDisconnectedBubbles());
    }

    private IEnumerator CheckSequence(Transform currentBubble)
    {
        yield return new WaitForSeconds(0.1f); // Tunggu sebentar sebelum mengecek

        sequenceBubbles.Clear();
        CheckBubbleSequence(currentBubble);

        // Jika sequence mencukupi jumlah, bubble meledak
        if (sequenceBubbles.Count >= SEQUENCE_SIZE)
        {
            foreach (Transform bubble in sequenceBubbles)
            {
                UnregisterBubble(bubble);
                Destroy(bubble.gameObject); // Hapus bubble dari game
            }
        }

        sequenceBubbles.Clear(); // Reset sequence untuk turn berikutnya
    }

    private void CheckBubbleSequence(Transform currentBubble)
    {
        sequenceBubbles.Add(currentBubble);

        Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
        List<Transform> neighbours = bubbleScript.GetNeighbours();

        foreach (Transform neighbour in neighbours)
        {
            if (!sequenceBubbles.Contains(neighbour))
            {
                Bubble neighbourScript = neighbour.GetComponent<Bubble>();
                if (neighbourScript.bubbleColor == bubbleScript.bubbleColor)
                {
                    CheckBubbleSequence(neighbour); // Lanjutkan pengecekan ke tetangga
                }
            }
        }
    }

    private IEnumerator ProcessDisconnectedBubbles()
    {
        yield return new WaitForSeconds(0.1f); // Tunggu sebentar sebelum memproses

        SetAllBubblesConnectionToFalse();
        SetConnectedBubblesToTrue();
        DropDisconnectedBubbles();
    }

    private void SetAllBubblesConnectionToFalse()
    {
        foreach (Transform bubble in activeBubbles)
        {
            bubble.GetComponent<Bubble>().isConnected = false;
        }
    }

    private void SetConnectedBubblesToTrue()
    {
        Queue<Transform> queue = new Queue<Transform>();

        foreach (Transform bubble in activeBubbles)
        {
            Bubble bubbleScript = bubble.GetComponent<Bubble>();
            if (bubbleScript.isFixed)
            {
                bubbleScript.isConnected = true;
                queue.Enqueue(bubble);
            }
        }

        while (queue.Count > 0)
        {
            Transform current = queue.Dequeue();
            Bubble currentScript = current.GetComponent<Bubble>();

            foreach (Transform neighbour in currentScript.GetNeighbours())
            {
                Bubble neighbourScript = neighbour.GetComponent<Bubble>();
                if (!neighbourScript.isConnected && neighbourScript.isFixed)
                {
                    neighbourScript.isConnected = true;
                    queue.Enqueue(neighbour);
                }
            }
        }
    }

    private void DropDisconnectedBubbles()
    {
        bubblesToDrop.Clear();

        foreach (Transform bubble in activeBubbles)
        {
            Bubble bubbleScript = bubble.GetComponent<Bubble>();
            if (!bubbleScript.isConnected)
            {
                bubblesToDrop.Add(bubble);
            }
        }

        foreach (Transform bubble in bubblesToDrop)
        {
            UnregisterBubble(bubble);
            bubble.SetParent(null);
            Rigidbody2D rb = bubble.gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1; // Atur gravitasi untuk bubble jatuh
            Destroy(bubble.gameObject, 2f); // Hancurkan setelah jatuh
        }
    }

    private void CheckGameOverCondition()
    {
        // Jika permainan sudah berakhir, langsung keluar
        if (isOver || gameOverLimit == null)
            return;

        foreach (Transform bubble in activeBubbles)
        {
            // Cek jika bubble melewati batas
            if (bubble.position.y <= gameOverLimit.position.y)
            {
                GameOver();
                return;
            }
        }
    }

    private void GameOver()
    {
        isOver = true;
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        Debug.Log("Game Over! Bubbles reached the limit.");
    }


    private void CheckWinCondition(int childCount)
    {
        if (childCount == 0)
        {
            Win();
        }
    }

    private void Win()
    {
        Time.timeScale = 0;
        continuePanel.SetActive(true);
        Debug.Log("You Win! All bubbles are cleared.");
    }
}
