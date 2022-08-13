using UnityEngine;

public class GatherQuest : MonoBehaviour
{
    private QuestGiver questGiver;
    [SerializeField] private QuestNumber questNumber;

    private void Awake()
    {
        questGiver = GameObject.FindGameObjectWithTag(questNumber.ToString()).GetComponent<QuestGiver>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            questGiver.quest.goal.ItemCollected();
            CheckIfComplete();
            Destroy(gameObject);
        }
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
