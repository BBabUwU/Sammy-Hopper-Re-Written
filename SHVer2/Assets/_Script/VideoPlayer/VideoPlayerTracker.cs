using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoPlayerTracker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Slider audioVolume;
    [SerializeField] private Slider progressTracker;
    bool usingSlider = false;

    private void Update()
    {
        if (!usingSlider && videoPlayer.isPlaying)
        {
            progressTracker.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }
    }

    public void Volume()
    {
        videoPlayer.SetDirectAudioVolume(0, audioVolume.value);
    }

    public void OnPointerDown(PointerEventData a)
    {
        usingSlider = true;
    }

    public void OnPointerUp(PointerEventData a)
    {
        float frame = (float)progressTracker.value * (float)videoPlayer.frameCount;
        videoPlayer.frame = (long)frame;
        usingSlider = false;
    }
}
