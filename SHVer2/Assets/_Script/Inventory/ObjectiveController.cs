using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    [HideInInspector] public QuestGiver questGiver;
    public int objectiveNumber;
    public TextMeshProUGUI objectNumber;
    public TextMeshProUGUI descMessage;
    public TextMeshProUGUI goalMessage;
    public TextMeshProUGUI rewardMessage;

    private void OnEnable()
    {
        UpdateObjective();
    }

    private void UpdateObjective()
    {
        int requiredAmount = questGiver.quest.goal.requiredAmount;
        int currentAmount = questGiver.quest.goal.currentAmount;

        objectNumber.text = "Objective " + questGiver.quest.questID;

        descMessage.text = questGiver.quest.description;

        if (questGiver.quest.completed)
        {
            goalMessage.text = "Quest Complete";
        }
        else
        {
            goalMessage.text = string.Format("{0} / {1}", currentAmount, requiredAmount);
        }

        rewardMessage.text = questGiver.quest.Reward;
    }
}