using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public List<QuestGiver> QuestList;

    ///<summary>
    /// Tracks the progress of quests
    ///</summary>

    private void AddQuest(QuestGiver quest)
    {
        QuestList.Add(quest);
    }

    private List<QuestGiver> GetQuest()
    {
        return QuestList;
    }

    private void OnEnable()
    {
        Actions.ListQuest += GetQuest;
        Actions.addToList += AddQuest;
    }

    private void OnDisable()
    {
        Actions.ListQuest -= GetQuest;
        Actions.addToList -= AddQuest;
    }
}
