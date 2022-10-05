using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class Tutorial : MonoBehaviour
{
    public List<Sprite> images;
    public int pageID;
    public bool isUnlocked = false;
    private Button button;
    public TutorialController tutorialSlider;

    public static event Func<List<QuestGiver>> ListQuest;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void OnEnable()
    {
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        if (ListQuest != null)
        {
            foreach (var item in ListQuest())
            {
                if (pageID == item.quest.questID && item.quest.completed) isUnlocked = true;
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
