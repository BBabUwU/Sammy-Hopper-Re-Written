using UnityEngine;

public class KillQuest : MonoBehaviour
{
    private QuestGiver questGiver;
    [SerializeField] private QuestNumber questNumber;

    private void Awake()
    {
        questGiver = GameObject.FindGameObjectWithTag(questNumber.ToString()).GetComponent<QuestGiver>();
    }

    private void OnDestroy()
    {
        questGiver.quest.goal.EnemyKilled();
        CheckIfComplete();
    }

    private void CheckIfComplete()
    {
        questGiver.quest.Evaluate();

        if (questGiver.quest.completed)
        {
            questGiver.QuestComplete();
        }
    }
}
