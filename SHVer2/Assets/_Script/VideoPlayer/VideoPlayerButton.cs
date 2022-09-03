using UnityEngine;
using UnityEngine.UI;
using System;

public enum VideoButtonType
{
    PlayButton,
    PauseButton
}

public class VideoPlayerButton : MonoBehaviour
{
    [SerializeField] private VideoButtonType videoButtonType;
    private Button button;
    public static event Action<VideoButtonType> videoButton;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonOnClick);
    }

    public void ButtonOnClick()
    {
        videoButton?.Invoke(videoButtonType);
    }
}
