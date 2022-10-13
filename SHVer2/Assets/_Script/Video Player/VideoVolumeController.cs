using UnityEngine;
using UnityEngine.UI;


public class VideoVolumeController : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float _value)
    {
        Actions.videoPlayer().SetDirectAudioVolume(0, _value);
    }
}
