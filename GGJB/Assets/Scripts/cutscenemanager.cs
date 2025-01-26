using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    public void ContinueToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
