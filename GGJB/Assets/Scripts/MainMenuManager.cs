using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Panel List")]
    public GameObject MainMenuPanel;
    public GameObject Setting;
    public GameObject levelSelection;
    private AudioSource audioSource;

    void Start()
    {

        MainMenuPanel.SetActive(true);
        levelSelection.SetActive(false);
        Setting.SetActive(false);

        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        if (GameData.instance.isMusicOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

    }
    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            GameData.instance.isMusicOn = false;
        }
        else
        {
            audioSource.Play();
            GameData.instance.isMusicOn = true;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
    public void LevelSelection()
    {
        //GameData.instance.isSinglePlayer = true;
        levelSelection.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void BackButton()
    {
        levelSelection.SetActive(false);
        SoundManager.instance.UIClickSfx();

    }
    public void SelectBallButton()
    {
        SoundManager.instance.UIClickSfx();
    }


    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
        SoundManager.instance.UIClickSfx();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
