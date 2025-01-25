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

    private float musicVolume = 0.5f; 
    private float sfxVolume = 0.5f;   

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

    private void Start()
    {
        ApplyVolumeSettings();
    }

    public void ChangeVolume()
    {
        if (volumeSlider != null)
        {
            musicVolume = volumeSlider.value; 
            if (audioSource != null)
            {
                audioSource.volume = musicVolume;
            }
        }
    }

    public void ChangeSFX()
    {
        if (SFXSlider != null)
        {
            sfxVolume = SFXSlider.value; 
            if (sfxAudioSource != null)
            {
                sfxAudioSource.volume = sfxVolume;
            }
        }
    }

    public void UpdateSliders(Slider newVolumeSlider, Slider newSFXSlider)
    {
        volumeSlider = newVolumeSlider;
        SFXSlider = newSFXSlider;

        ApplyVolumeSettings();

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        }

        if (SFXSlider != null)
        {
            SFXSlider.onValueChanged.AddListener(delegate { ChangeSFX(); });
        }
    }

    private void ApplyVolumeSettings()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = musicVolume;
        }

        if (audioSource != null)
        {
            audioSource.volume = musicVolume;
        }

        if (SFXSlider != null)
        {
            SFXSlider.value = sfxVolume;
        }

        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = sfxVolume;
        }
    }
}
