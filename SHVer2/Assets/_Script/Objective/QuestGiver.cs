using UnityEngine;
using System;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public GameObject blockedPath;

    private void DestroyPath(int id)
    {
        if (id == quest.questID) Destroy(blockedPath);
    }

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

    public void CheckIfFinish(int questNumber)
    {
        if (questNumber == quest.questID)
        {
            quest.Evaluate();
        }
    }

    private void OnEnable()
    {
        KillQuest.enemyKilled += AddKillCounter;
        GatherQuest.itemCollected += AddGatherCounter;
        Quest.clearPath += DestroyPath;
    }

    private void OnDisable()
    {
        KillQuest.enemyKilled -= AddKillCounter;
        GatherQuest.itemCollected -= AddGatherCounter;
        Quest.clearPath -= DestroyPath;
    }
}
