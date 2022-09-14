using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private VideoClip vidClip;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = vidClip;
    }

    private void OnButtonClicked(VideoButtonType _videoButtonType)
    {
        if (_videoButtonType == VideoButtonType.PlayButton) videoPlayer.Play();
        if (_videoButtonType == VideoButtonType.PauseButton) videoPlayer.Pause();
        if (_videoButtonType == VideoButtonType.CloseButton)
        {
            videoPlayer.Stop();
            UIManager.Instance.TurnOnUI(UIType.VideoMenu);
            UIManager.Instance.TurnOffUI(UIType.VideoPlayer);
        }

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
        VideoSetup.videoPlayer += GetVideoPlayerComponent;
    }

    private void OnDisable()
    {
        VideoButtonController.videoButton -= OnButtonClicked;
        VideoVolumeController.videoPlayer -= GetVideoPlayerComponent;
        VideoSetup.videoPlayer -= GetVideoPlayerComponent;
    }
}
