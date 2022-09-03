using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{

    [SerializeField] private VideoButtonType videoButtonType;
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnButtonClicked(VideoButtonType _videoButtonType)
    {
        if (_videoButtonType == VideoButtonType.PlayButton) videoPlayer.Play();
        if (_videoButtonType == VideoButtonType.PauseButton) videoPlayer.Pause();
    }

    private void OnEnable()
    {
        VideoPlayerButton.videoButton += OnButtonClicked;
    }

    private void OnDisable()
    {
        VideoPlayerButton.videoButton -= OnButtonClicked;
    }
}
