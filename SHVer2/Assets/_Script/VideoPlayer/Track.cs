using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class Track : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public VideoPlayer audioS;
    public Slider audioVolume;

    public VideoPlayer video;
    Slider tracking;
    bool slide = false;

    private void Start()
    {
        tracking = GetComponent<Slider>();
    }

    private void Update()
    {
        if (!slide && video.isPlaying)
        {
            tracking.value = (float)video.frame / (float)video.frameCount;
        }

    }

    public void Volume()
    {
        audioS.SetDirectAudioVolume(0, audioVolume.value);
    }

    public void OnPointerDown(PointerEventData a)
    {
        slide = true;
    }

    public void OnPointerUp(PointerEventData a)
    {
        float frame = (float)tracking.value * (float)video.frameCount;
        video.frame = (long)frame;
        slide = false;
    }
}
