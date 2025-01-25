using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton instance

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider SFXSlider;
    public AudioSource audioSource;
    public AudioSource sfxAudioSource; 
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }
        load();
    }

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        audioSource.volume = volume;
        saveVolume();
    }
    public void ChangeSFX()
    {
        float sfx = SFXSlider.value;
        sfxAudioSource.volume = sfx;
        saveSFX();
    }
    public void load()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume");
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;

        float savedSFX = PlayerPrefs.GetFloat("sfxVolume");
        SFXSlider.value = savedSFX;
        sfxAudioSource.volume = savedSFX;
    }

    public void saveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
       }
    public void saveSFX()
    {
        PlayerPrefs.SetFloat("sfxVolume", SFXSlider.value);
    }
}
