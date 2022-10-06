using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class Tutorial : MonoBehaviour
{
    public List<Sprite> images;
    private Image image;
    public int questID;
    public bool isUnlocked = false;
    public bool hasRead = false;
    private Button button;
    public TutorialController tutorialSlider;
    public static event Func<List<QuestGiver>> ListQuest;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void OnEnable()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        image.enabled = false;

        if (ListQuest != null)
        {
            foreach (var item in ListQuest())
            {
                if (questID == item.quest.questID && item.quest.completed)
                {
                    image.enabled = true;
                    isUnlocked = true;
                }
            }
        }
    }

    private void ButtonClicked()
    {
        if (isUnlocked)
        {
            tutorialSlider.currentTutorial = this;
            UIManager.Instance.TurnOnUI(UIType.TutorialWindow);
        }
    }
}
