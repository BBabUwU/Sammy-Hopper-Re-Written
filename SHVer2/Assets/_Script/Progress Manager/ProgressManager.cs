using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject blockedArea;
    public int answeredQuizzes = 0;
    List<Quest> questList = new List<Quest>();
    private void QuestComplete(Quest quest)
    {
        questList.Add(quest);
    }

    private void QuizCompleted()
    {
        answeredQuizzes++;
    }

    private void PuzzleCompleted()
    {
        Destroy(blockedArea);
    }

    private int GetNumberOfAnsweredQuizzes()
    {
        return answeredQuizzes;
    }

    private void OnEnable()
    {
        PuzzleManager.PuzzleIsComplete += PuzzleCompleted;

        ExplorationQuiz.QuizComplete += QuizCompleted;

        PuzzleManager.quizzesAnswered += GetNumberOfAnsweredQuizzes;
    }

    private void OnDisable()
    {
        PuzzleManager.PuzzleIsComplete -= PuzzleCompleted;

        ExplorationQuiz.QuizComplete -= QuizCompleted;

        PuzzleManager.quizzesAnswered -= GetNumberOfAnsweredQuizzes;
    }
}
