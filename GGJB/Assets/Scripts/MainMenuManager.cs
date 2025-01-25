using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Panel List")]
    public GameObject MainMenuPanel;
    public GameObject Setting;
    public GameObject levelSelection;
    private AudioSource audioSource;
    private SFXManager sfxmanager;
    void Start()
    {

        MainMenuPanel.SetActive(true);
        levelSelection.SetActive(false);
        Setting.SetActive(false);
        sfxmanager = FindObjectOfType<SFXManager>();

        //audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        //if (GameData.instance.isMusicOn)
        //{
        //    audioSource.Play();
        //}
        //else
        //{
        //    audioSource.Pause();
        //}

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

    public void LevelSelection()
    {
        if (sfxmanager != null)
        {
            Debug.Log("Playing UI Click SFX");
            sfxmanager.UIClickSfx();
        }
        levelSelection.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void Settings()
    {
        Setting.SetActive(true);
        MainMenuPanel.SetActive(false);

    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void BackButton()
    {
 
            MainMenuPanel.SetActive(true);
            levelSelection.SetActive(false);
            Setting.SetActive(false);
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
