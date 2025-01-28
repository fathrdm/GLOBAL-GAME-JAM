using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cts4 : MonoBehaviour
{
    private Animator cutsceneAnimator;

    private void Start()
    {
        Time.timeScale = 1;
        cutsceneAnimator = GetComponent<Animator>();

        cutsceneAnimator.Play("cs5");
        cutsceneAnimator.SetTrigger("play");
    }

    public void ShowButton()
    {
        ContinueUnlockLevel();
    }

    public void ContinueUnlockLevel()
    {

        SceneManager.LoadScene("LevelAkhir");

        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();

        }

    }
}
