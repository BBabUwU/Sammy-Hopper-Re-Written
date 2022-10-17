using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Music_volume"))
        {
            PlayerPrefs.SetFloat("Music_volume", 1);
            Load();
        }
        else
        {
            Load();
        }

    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Music_volume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Music_volume", volumeSlider.value);
    }
}
