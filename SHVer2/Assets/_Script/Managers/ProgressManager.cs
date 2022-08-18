using UnityEngine;
using System;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    public bool quest1Completed;
    public bool quest2Completed;
    public bool quiz1Completed;
    public bool quiz2Completed;
    public bool quiz3Completed;


    private void Awake()
    {
        Instance = this;
    }

    public void QuestIsFinished(QuestNumber questName)
    {
        if (questName == QuestNumber.Quest1)
        {
            quest1Completed = true;
        }

        if (questName == QuestNumber.Quest2)
        {
            quest2Completed = true;
        }
    }

    public void QuizIsFinished(QuizNumber quizNumber)
    {
        if (quizNumber == QuizNumber.Quiz1)
        {
            quiz1Completed = true;
        }

        if (quizNumber == QuizNumber.Quiz2)
        {
            quiz2Completed = true;
        }

        if (quizNumber == QuizNumber.Quiz3)
        {
            quiz3Completed = true;
        }
    }

    public bool CheckIfFinished(QuestNumber questNumber)
    {
        bool isFinished = false;

        if (questNumber == QuestNumber.Quest1)
        {
            return isFinished = quest1Completed;
        }

        if (questNumber == QuestNumber.Quest2)
        {
            return isFinished = quest2Completed;
        }

        return isFinished;
    }

    public bool CheckIfFinished(QuizNumber quizNumber)
    {
        bool isFinished = false;

        if (quizNumber == QuizNumber.Quiz1)
        {
            return isFinished = quiz1Completed;
        }

        if (quizNumber == QuizNumber.Quiz2)
        {
            return isFinished = quiz2Completed;
        }

        if (quizNumber == QuizNumber.Quiz3)
        {
            return isFinished = quiz3Completed;
        }

        return isFinished;
    }
}

public enum QuestNumber
{
    Quest1,
    Quest2
}

public enum QuizNumber
{
    Quiz1,
    Quiz2,
    Quiz3
}
