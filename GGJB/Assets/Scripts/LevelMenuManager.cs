using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        string achievementKey = "Level" + levelId + "_AchievementSeen";
        if (PlayerPrefs.GetInt(achievementKey, 0) == 0) // Cek apakah Achievement scene belum dilihat
        {
            PlayerPrefs.SetInt(achievementKey, 1); // Tandai Achievement scene sebagai sudah dilihat
            PlayerPrefs.Save();
            SceneManager.LoadScene("Achievement"); // Arahkan ke scene Achievement
        }
        else
        {
            string levelname = "Level " + levelId;
            SceneManager.LoadScene(levelname); // Langsung ke level jika Achievement sudah dilihat
        }
    }
}
