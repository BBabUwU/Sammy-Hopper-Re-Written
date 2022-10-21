using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public List<QuestGiver> QuestList;
    public List<int> additionalInfoIndex;
    private int extrasCounter = 0;

    ///<summary>
    /// Tracks the progress of quests
    ///</summary>

    private void AddQuest(QuestGiver quest)
    {
        QuestList.Add(quest);
    }

    private void AddAdditionalInfoIndex()
    {
        extrasCounter++;
        additionalInfoIndex.Add(extrasCounter);
    }

    private List<QuestGiver> GetQuest()
    {
        return QuestList;
    }

    private List<int> GetAI()
    {
        return additionalInfoIndex;
    }

    private void OnEnable()
    {
        Actions.ListQuest += GetQuest;
        Actions.addToList += AddQuest;
        Actions.ExtrasList += GetAI;
        Actions.IncrementExtra += AddAdditionalInfoIndex;
    }

    private void OnDisable()
    {
        Actions.ListQuest -= GetQuest;
        Actions.addToList -= AddQuest;
        Actions.ExtrasList -= GetAI;
        Actions.IncrementExtra += AddAdditionalInfoIndex;
    }
}
