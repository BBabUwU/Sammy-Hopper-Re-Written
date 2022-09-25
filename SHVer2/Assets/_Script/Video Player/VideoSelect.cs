using UnityEngine;
using UnityEngine.UI;
using System;

public class VideoSelect : MonoBehaviour
{
    public int videoNumber;
    private Button button;
    public static event Action<int> ChangeClip;
    public bool isLocked;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetClip);
    }

    private void SetClip()
    {
        if (!isLocked)
        {
            ChangeClip?.Invoke(videoNumber);
            UIManager.Instance.TurnOnUI(UIType.VideoPlayer);
            UIManager.Instance.TurnOffUI(UIType.VideoMenu);
        }
    }
}
