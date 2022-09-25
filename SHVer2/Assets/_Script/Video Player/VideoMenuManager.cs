using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VideoMenuManager : MonoBehaviour
{
    public static event Action MenuOpened;
    public List<VideoSelect> videoButtons;
    private void UnclockVideo(int unlockedIndex)
    {
        foreach (var item in videoButtons)
        {
            if (item.videoNumber == unlockedIndex)
            {
                item.isLocked = false;
            }
        }
    }

    private void OnEnable()
    {
        MenuOpened?.Invoke();
    }
}
