using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private bool quest1;
    [SerializeField] private bool quest2;
    [SerializeField] private bool quiz1;
    [SerializeField] private bool quiz2;
    [SerializeField] private bool bossBattle;

    public void QuestIsFinished(string tagName)
    {
        if (tagName == "Quest1")
        {
            quest1 = true;
        }

        if (tagName == "Quest2")
        {
            quest2 = true;
        }

        if (tagName == "Quiz1")
        {
            quiz1 = true;
        }

        if (tagName == "Quiz2")
        {
            quiz2 = true;
        }

        if (tagName == "BossBattle")
        {
            bossBattle = true;
        }
    }

    public bool QuestIsCompleted(string tagName)
    {
        bool isPassed = false;

        if (tagName == "Quest1")
        {
            isPassed = quest1;
        }

        if (tagName == "Quest2")
        {
            isPassed = quest2;
        }

        if (tagName == "Quiz1")
        {
            isPassed = quiz1;
        }

        if (tagName == "Quiz2")
        {
            isPassed = quiz2;
        }

        if (tagName == "BossBattle")
        {
            isPassed = bossBattle;
        }

        return isPassed;
    }
}
public enum QuestNumber
{
    Quest1,
    Quest2
}
