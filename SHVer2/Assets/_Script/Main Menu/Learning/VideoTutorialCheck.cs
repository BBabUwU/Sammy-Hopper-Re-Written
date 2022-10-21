using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoTutorialCheck : MonoBehaviour
{
    public string stage;
    [SerializeField] List<GameObject> buttons;

    private void Start()
    {
        Activate_Buttons();
    }
    private void Activate_Buttons()
    {
        int completeLevel = PlayerPrefs.GetInt("levelAt", 0) - 1;

        if (stage == "stage1" && completeLevel == 1)
        {
            foreach (var button in buttons)
            {
                button.SetActive(true);
            }
        }
        else
        {
            foreach (var button in buttons)
            {
                button.SetActive(false);
            }
        }
    }
}
