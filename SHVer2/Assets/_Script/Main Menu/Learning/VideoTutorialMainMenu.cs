using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Video;

public class VideoTutorialMainMenu : MonoBehaviour
{
    public VideoClip videoTutorial;
    public VideoPlayer videoPlayer;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickVideoPlayerWindow);
    }

    private void OnClickVideoPlayerWindow()
    {
        videoPlayer.clip = videoTutorial;
        videoPlayer.gameObject.SetActive(true);
    }
}
