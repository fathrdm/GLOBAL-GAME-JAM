using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public enum EndingStage
{
    Ggj25,
    HappyEnding,
    BadEnding,
}

public class EndingManager5 : MonoBehaviour
{

    private static EndingManager5 instance;
    [Header("Ending")]
    public TextMeshProUGUI ScoreText;
    public EndingStage Ending;
    public int TimerMinit;
    public float TimerDetik;

    [Header("Game Setting")]
    public bool isOver;
    public bool isPause;

    [Header("Panels")]
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject settingsMenuPanel;

    [Header("Audio")]
    [SerializeField] AudioSource audiorain;

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

    }

    private void Start()
    {
        Time.timeScale = 1;
        // Buat panel
        PausePanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        isPause = false;
        isOver = false;
        audiorain = GameObject.Find("BGM").GetComponent<AudioSource>();
        Ending = EndingStage.Ggj25;
        ScoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        GamePause();

        TimerDetik = TimerDetik + 1 * Time.deltaTime;
        if (TimerDetik >= 60) 
        {
            TimerMinit += 1;
            TimerDetik = 0;
        }

        var scoreToInt = Convert.ToInt32(ScoreText.text);

        if (TimerMinit >= 3 && scoreToInt <= 20) 
        {
            Ending = EndingStage.HappyEnding;
        }
        else if(TimerMinit >= 3 && scoreToInt >= 20)
        {
            Ending = EndingStage.BadEnding;
        }

        switch (Ending)
        {
            case EndingStage.HappyEnding:
                Ending = EndingStage.HappyEnding;
                Debug.Log("Happy");
                break;
            case EndingStage.BadEnding:
                Ending = EndingStage.BadEnding;
                Debug.Log("BadEnding");
                break;
        }
    }

    //===============UI PANEL==================//
    public void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;

            if (isPause)
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
                audiorain.Pause();  
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
        audiorain.UnPause();

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

}
