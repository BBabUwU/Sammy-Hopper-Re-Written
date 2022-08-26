using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerSetup : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip tutorialClip_1;
    [SerializeField] private VideoClip tutorialClip_2;

    public void ChooseVideo1()
    {
        videoPlayer.clip = tutorialClip_1;
    }

    public void ChooseVideo2()
    {
        videoPlayer.clip = tutorialClip_2;
    }
}
