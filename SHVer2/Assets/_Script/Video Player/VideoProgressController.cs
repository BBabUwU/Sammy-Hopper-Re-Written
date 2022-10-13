using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VideoProgressController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Slider slider;
    private bool usingSlider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (!usingSlider && Actions.videoPlayer().isPlaying)
        {
            slider.value = (float)Actions.videoPlayer().frame / (float)Actions.videoPlayer().frameCount;
        }
    }

    public void OnPointerDown(PointerEventData a)
    {
        usingSlider = true;
    }

    public void OnPointerUp(PointerEventData a)
    {
        float frame = (float)slider.value * (float)Actions.videoPlayer().frameCount;
        Actions.videoPlayer().frame = (long)frame;
        usingSlider = false;
    }
}
