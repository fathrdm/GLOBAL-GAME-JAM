using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSliders : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
            SoundManager.instance.UpdateSliders(volumeSlider, sfxSlider);
    }
}
