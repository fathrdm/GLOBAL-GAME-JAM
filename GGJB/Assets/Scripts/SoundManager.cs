using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton instance

    [SerializeField] private Slider volumeSlider;
    public AudioSource audioSource;

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
        load();
    }

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        audioSource.volume = volume;
        save();
    }

    public void load()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume");
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;
    }

    public void save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
