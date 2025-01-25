using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSFXManager : MonoBehaviour
{
    public static AudioSFXManager instance;

    public AudioClip uiButton;
    public AudioClip ballBounce;
    public AudioClip goal;
    public AudioClip gameOver;

    private new AudioSource audio;
    // Start is called before the first frame update

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameOver);
        }
        else
        {
            instance = this;
            audio = GetComponent<AudioSource>();
        }
    }

    public void UIClickSfx()
    {
        audio.PlayOneShot(uiButton);
    }

    public void BallBounceSfx()
    {
        audio.PlayOneShot(ballBounce);
    }

    public void GoalSfx()
    {
        audio.PlayOneShot(goal);
    }

    public void GameOverSfx()
    {
        audio.PlayOneShot(gameOver);
    }
}
