using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoinPerLevel : MonoBehaviour
{
    public void ContinueUnlockLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        // Periksa apakah ini adalah level terakhir yang telah dicapai
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }

        // Tambahkan logika untuk Achievement scene
        string achievementKey = currentLevel + "_AchievementSeen";
        if (PlayerPrefs.GetInt(achievementKey, 0) == 0) // Cek apakah Achievement scene belum dilihat
        {
            PlayerPrefs.SetInt(achievementKey, 1); // Tandai Achievement scene sebagai sudah dilihat
            PlayerPrefs.Save();
            SceneManager.LoadScene("Achievement"); // Arahkan ke scene Achievement
        }
        else
        {
            // Jika Achievement sudah dilihat, lanjut ke level berikutnya
            SceneManager.LoadScene("Level 2");
        }
    }
}
