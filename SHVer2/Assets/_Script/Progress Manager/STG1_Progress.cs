using System.Collections.Generic;
using UnityEngine;

public class STG1_Progress : MonoBehaviour
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
        ObjectiveManager.ListQuest += GetQuest;

        Tutorial.ListQuest += GetQuest;

        InteractionDialogue.addToList += AddQuest;
    }

    private void OnDisable()
    {
        ObjectiveManager.ListQuest -= GetQuest;

        Tutorial.ListQuest -= GetQuest;

        InteractionDialogue.addToList -= AddQuest;
    }
}
