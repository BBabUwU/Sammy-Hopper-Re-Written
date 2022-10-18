using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;

    private void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt");

        for (int i = 0; i < levelAt; i++)
        {
            lvlButtons[i].interactable = true;
        }
    }
}
