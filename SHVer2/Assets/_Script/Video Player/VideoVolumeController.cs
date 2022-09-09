using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Video;

public class VideoVolumeController : MonoBehaviour
{
    private Slider slider;
    public static event Func<VideoPlayer> videoPlayer;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float _value)
    {
        videoPlayer().SetDirectAudioVolume(0, _value);
    }
}
