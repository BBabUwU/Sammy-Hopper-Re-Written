using UnityEngine;
using System;

public class QuestGiver : MonoBehaviour
{
    public QuestNumber questNumber;
    public Quest quest;

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
            ProgressManager.Instance.QuestIsFinished(questNumber);
        }
    }

    private void AddCounter(QuestNumber questNumber)
    {
        if (questNumber == this.questNumber)
        {
            quest.goal.ItemCollected();
            quest.goal.EnemyKilled();
            quest.Evaluate();
            QuestComplete();
        }
    }
}
