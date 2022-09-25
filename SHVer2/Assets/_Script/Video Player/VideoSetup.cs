using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoSetup : MonoBehaviour
{
    public VideoClip videoTutorial_1;
    public static event Func<VideoPlayer> videoPlayer;

    private void SetClip(int videoNumber)
    {
        if (videoNumber == 0)
            videoPlayer().clip = videoTutorial_1;
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
