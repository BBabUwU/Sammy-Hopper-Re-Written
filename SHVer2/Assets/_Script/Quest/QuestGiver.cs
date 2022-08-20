using UnityEngine;
using System;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public static event Action<Quest> AddQuest;
    public static event Action<Quest> QuestPassed;

    private void Awake()
    {
        AddQuest?.Invoke(quest);
    }

    private void OnEnable()
    {
        GatherQuest.itemCollected += AddGatherCounter;
        KillQuest.enemyKilled += AddKillCounter;
        GatherQuest.itemCollected += CheckIfFinish;
        KillQuest.enemyKilled += CheckIfFinish;
    }

    private void OnDisable()
    {
        GatherQuest.itemCollected -= AddGatherCounter;
        KillQuest.enemyKilled -= AddKillCounter;
        GatherQuest.itemCollected -= CheckIfFinish;
        KillQuest.enemyKilled -= CheckIfFinish;
    }

    public void QuestComplete()
    {
        if (quest.completed)
        {
            QuestPassed?.Invoke(quest);
        }
    }

    private void AddKillCounter(int questNumber)
    {
        if (questNumber == quest.questNumber)
        {
            quest.goal.EnemyKilled();
        }
    }

    private void AddGatherCounter(int questNumber)
    {
        if (questNumber == quest.questNumber)
        {
            quest.goal.ItemCollected();
        }
    }

    private void CheckIfFinish(int questNumber)
    {
        if (questNumber == quest.questNumber)
        {
            quest.Evaluate();
            QuestComplete();
        }
    }
}
