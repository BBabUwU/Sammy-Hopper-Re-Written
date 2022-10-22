using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    private Image image;
    public int questID;
    public VideoClip videoTutorial;
    public VideoPlayer videoPlayer;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickVideoPlayerWindow);
    }

    private void OnEnable()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        button.interactable = false;

        if (Actions.ListQuest != null)
        {
            foreach (var item in Actions.ListQuest())
            {
                if (questID == item.quest.questID && item.quest.completed)
                {
                    button.interactable = true;
                }
            }
        }
    }

    private void OnClickVideoPlayerWindow()
    {
        UIManager.Instance.TurnOnUI(UIType.VideoPlayer);
        videoPlayer.clip = videoTutorial;
    }
}
