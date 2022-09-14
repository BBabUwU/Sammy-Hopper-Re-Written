using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoSetup : MonoBehaviour
{
    public VideoClip videoTutorial_1;
    public VideoClip videoTutorial_2;
    public static event Func<VideoPlayer> videoPlayer;

    private void SetClip(VideoNumber videoNumber)
    {
        if (videoNumber == VideoNumber.Video1)
            videoPlayer().clip = videoTutorial_1;
        if (videoNumber == VideoNumber.Video2)
            videoPlayer().clip = videoTutorial_2;
    }

    private void OnEnable()
    {
        VideoSelect.ChangeClip += SetClip;
    }

    private void OnDisable()
    {
        VideoSelect.ChangeClip -= SetClip;
    }
}
