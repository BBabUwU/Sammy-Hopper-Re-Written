using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip vidClip;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = vidClip;
    }

    private void OnButtonClicked(VideoButtonType _videoButtonType)
    {
        if (_videoButtonType == VideoButtonType.PlayButton) videoPlayer.Play();
        if (_videoButtonType == VideoButtonType.PauseButton) videoPlayer.Pause();
        if (_videoButtonType == VideoButtonType.CloseButton) GameManager.Instance.UpdateGameState(GameState.Exploration);
    }

    private VideoPlayer GetVideoPlayerComponent()
    {
        return videoPlayer;
    }

    private void OnEnable()
    {
        VideoButtonController.videoButton += OnButtonClicked;
        VideoProgressController.videoPlayer += GetVideoPlayerComponent;
        VideoVolumeController.videoPlayer += GetVideoPlayerComponent;
    }

    private void OnDisable()
    {
        VideoButtonController.videoButton -= OnButtonClicked;
        VideoVolumeController.videoPlayer -= GetVideoPlayerComponent;
    }
}
