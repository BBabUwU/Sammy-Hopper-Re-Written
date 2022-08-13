using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private QuestManager questManager;
    public Quest quest;

    private void Awake()
    {
        questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    public void QuestComplete()
    {
        if (quest.completed)
        {
            questManager.QuestIsFinished(gameObject.tag);
        }
    }
}
