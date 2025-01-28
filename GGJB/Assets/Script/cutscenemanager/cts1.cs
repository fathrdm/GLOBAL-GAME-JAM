using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneManager1 : MonoBehaviour
{
    public Animator cutsceneAnimator;
   
    private void Start()
    {
        Time.timeScale = 1;
        cutsceneAnimator = GetComponent<Animator>();

        cutsceneAnimator.Play("cs2");
        cutsceneAnimator.SetTrigger("play");
    }

    public void ShowButton()
    {
        ContinueUnlockLevel();
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
}
