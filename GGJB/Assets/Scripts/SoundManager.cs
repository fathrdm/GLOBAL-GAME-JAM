using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        load();
    }

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        audioSource.volume = volume; // Atur volume AudioSource
        save();
    }

    public void load()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume");
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume; // Sinkronisasi volume AudioSource
    }

    public void save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
