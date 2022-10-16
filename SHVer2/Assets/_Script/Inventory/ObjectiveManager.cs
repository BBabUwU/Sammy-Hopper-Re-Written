using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class ObjectiveManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currentQuestText;
    [SerializeField] private TextMeshProUGUI currentDescriptionText;
    [SerializeField] private TextMeshProUGUI currentProgressText;
    [SerializeField] private TextMeshProUGUI currentRewardText;

    private void OnEnable()
    {
        if (Actions.ListQuest().Count != 0)
        {
            UpdateQuest();
        }
    }

    private void UpdateQuest()
    {
        QuestGiver currentQuest = Actions.ListQuest()[Actions.ListQuest().Count - 1];

        currentQuestText.text = "CURRENT QUEST";

        currentDescriptionText.text = "DESCRIPTION: " + currentQuest.quest.description;

        int currentCounter = currentQuest.quest.goal.currentAmount;
        int requiredCounter = currentQuest.quest.goal.requiredAmount;

        if (currentCounter < requiredCounter)
            currentProgressText.text = "PROGRESS: " + String.Format("{0} / {1}", currentCounter, requiredCounter);
        else if (currentCounter == requiredCounter && !currentQuest.quest.completed)
            currentProgressText.text = "PROGRESS " + String.Format("{0} / {1}", currentCounter, requiredCounter) + " GOAL REACHED!";
        else if (currentCounter == requiredCounter && currentQuest.quest.completed)
            currentProgressText.text = "QUEST COMPLETE!";


        currentRewardText.text = "REWARD " + currentQuest.quest.Reward;
    }
}