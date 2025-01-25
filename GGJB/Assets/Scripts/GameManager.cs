using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton untuk akses global

    [Header("Game Setting")]
    public bool isOver;
    public bool isPause;

    [Header("Panels")]
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject settingsMenuPanel;

    private const int SEQUENCE_SIZE = 3; // Jumlah bubble minimum untuk meledak

    private List<Transform> sequenceBubbles; // Menyimpan bubble dalam sequence
    //public GameObject explosionPrefab; // Efek ledakan

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
        //buat panel
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        isPause = false;
        isOver = false;

    }

    private void Update()
    {
        GamePause();
    }
    public void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
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
        settingsMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    // Mengecek bubble yang bersentuhan untuk sequence
    public void ProcessTurn(Transform currentBubble)
    {
        StartCoroutine(CheckSequence(currentBubble));
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
                // Membuat efek ledakan
                //Instantiate(explosionPrefab, bubble.position, Quaternion.identity);
                Destroy(bubble.gameObject); // Hapus bubble dari game
            }
        }

        sequenceBubbles.Clear(); // Reset sequence untuk turn berikutnya
    }

    // Rekursif untuk mengecek bubble dengan warna yang sama
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
}
