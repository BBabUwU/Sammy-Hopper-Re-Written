using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        playButton.onClick.AddListener(OnClickPlay);
        pauseButton.onClick.AddListener(OnClickPause);
        closeButton.onClick.AddListener(OnClickClose);
    }

    private void OnClickPlay()
    {
        videoPlayer.Play();
    }

    private void OnClickPause()
    {
        videoPlayer.Pause();
    }

    private void OnClickClose()
    {
        videoPlayer.Stop();
        UIManager.Instance.TurnOffUI(UIType.VideoPlayer);
        GameManager.Instance.UpdateGameState(GameState.Exploration);
    }


    private VideoPlayer GetVideoPlayerComponent()
    {
        return videoPlayer;
    }

    private void OnEnable()
    {
        Actions.videoPlayer += GetVideoPlayerComponent;
    }

    private void OnDisable()
    {
        Actions.videoPlayer += GetVideoPlayerComponent;
    }
}
