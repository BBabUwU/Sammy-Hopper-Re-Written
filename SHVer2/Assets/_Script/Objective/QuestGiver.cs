using UnityEngine;
using System;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    private void AddKillCounter(int questNumber)
    {
        if (questNumber == quest.questID)
        {
            quest.goal.EnemyKilled();
        }
    }

    private void AddGatherCounter(int questNumber)
    {
        if (questNumber == quest.questID)
        {
            quest.goal.ItemCollected();
        }
    }

    private void CheckIfFinish(int questNumber)
    {
        if (questNumber == quest.questID)
        {
            quest.Evaluate();
        }
    }
}
