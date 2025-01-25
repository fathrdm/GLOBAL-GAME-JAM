using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    public Button[] button;

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
    public void OpenLevel(int levelId)
    {
        string levelname = "Level " + levelId;
        SceneManager.LoadScene(levelname);
    }
}
