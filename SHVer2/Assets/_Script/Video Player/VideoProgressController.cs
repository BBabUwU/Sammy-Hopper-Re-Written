using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using System;

public class VideoProgressController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Slider slider;
    private bool usingSlider;
    public static event Func<VideoPlayer> videoPlayer;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (!usingSlider && videoPlayer().isPlaying)
        {
            slider.value = (float)videoPlayer().frame / (float)videoPlayer().frameCount;
        }
    }

    public void OnPointerDown(PointerEventData a)
    {
        usingSlider = true;
    }

    public void OnPointerUp(PointerEventData a)
    {
        float frame = (float)slider.value * (float)videoPlayer().frameCount;
        videoPlayer().frame = (long)frame;
        usingSlider = false;
    }
}
