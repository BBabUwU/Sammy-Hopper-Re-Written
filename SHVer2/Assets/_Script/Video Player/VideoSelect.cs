using UnityEngine;
using UnityEngine.UI;
using System;

public enum VideoNumber
{
    Video1,
    Video2
}

public class VideoSelect : MonoBehaviour
{
    [SerializeField] private VideoNumber videoNumber;
    private Button button;

    public static event Action<VideoNumber> ChangeClip;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetClip);
    }

    private void SetClip()
    {
        ChangeClip?.Invoke(videoNumber);
        UIManager.Instance.TurnOnUI(UIType.VideoPlayer);
        UIManager.Instance.TurnOffUI(UIType.VideoMenu);
    }
}
