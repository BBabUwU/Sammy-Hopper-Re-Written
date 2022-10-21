using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private bool isMainMenu = false;

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
        Actions.playAudio?.Invoke("stg1_bgmusic");

        if (!isMainMenu)
        {
            UIManager.Instance.TurnOffUI(UIType.VideoPlayer);
            GameManager.Instance.UpdateGameState(GameState.Exploration);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    private VideoPlayer GetVideoPlayerComponent()
    {
        return videoPlayer;
    }

    private void OnEnable()
    {
        Actions.pauseAudio?.Invoke("stg1_bgmusic");
        Actions.videoPlayer += GetVideoPlayerComponent;
    }

    private void OnDisable()
    {
        Actions.videoPlayer += GetVideoPlayerComponent;
    }
}
