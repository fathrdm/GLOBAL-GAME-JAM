using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager5 : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip[] AudioClip;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void Sound_1()
    {
        Audio.clip = AudioClip[0];
        Audio.Play();
    }

    public void Sound_2()
    {
        Audio.clip = AudioClip[1];
        Audio.Play();
    }
}
