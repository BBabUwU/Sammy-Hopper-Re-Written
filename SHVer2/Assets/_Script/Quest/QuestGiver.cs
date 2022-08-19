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
        GatherQuest.itemCollected += AddCounter;
        KillQuest.enemyKilled += AddCounter;
    }

    private void OnDisable()
    {
        GatherQuest.itemCollected -= AddCounter;
        KillQuest.enemyKilled -= AddCounter;
    }

    public void QuestComplete()
    {
        if (quest.completed)
        {
            QuestPassed?.Invoke(quest);
        }
    }

    private void AddCounter(int questNumber)
    {
        if (questNumber == quest.questNumber)
        {
            quest.goal.ItemCollected();
            quest.goal.EnemyKilled();
            quest.Evaluate();
            QuestComplete();
        }
    }
}
