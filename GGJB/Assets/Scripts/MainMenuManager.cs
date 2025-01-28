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
    //private GameObject levelSelection;
    private AudioSource audioSource;
    private SFXManager sfxmanager;
    void Start()
    {
        MainMenuPanel.SetActive(true);
        Setting.SetActive(false);
        sfxmanager = FindObjectOfType<SFXManager>();
    }
    public void LevelSelection()
    {
        sfxmanager.UIClickSfx();
        SceneManager.LoadScene("CutScene1");
    }

    public void Settings()
    {
        sfxmanager.UIClickSfx();
        Setting.SetActive(true);
        MainMenuPanel.SetActive(false);

    }

    public void PlayButton()
    {
        sfxmanager.UIClickSfx();
        SceneManager.LoadScene("CutScene1");
    }
    public void BackButton()
    {
          MainMenuPanel.SetActive(true);
        sfxmanager.UIClickSfx();
        Setting.SetActive(false);
    }
    public void ExitGame()
    {
        sfxmanager.UIClickSfx();
        Application.Quit();
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs telah direset.");
    }
}
