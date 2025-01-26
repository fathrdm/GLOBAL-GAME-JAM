using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class LevelMenuManager : MonoBehaviour
{
    public Button[] button;
    private SFXManager sfxmanager;
    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            button[i].interactable = true;
        }
    }

    private void Start()
    {
        sfxmanager = FindObjectOfType<SFXManager>();
    }
    public void OpenLevel(int levelId)
    {
        sfxmanager.UIClickSfx();
        string levelname = "Level " + levelId;
        SceneManager.LoadScene(levelname);
    }
}