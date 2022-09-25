using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject blockedArea;
    public int answerQuizzes = 0;

    private void QuizCompleted()
    {
        answerQuizzes++;
    }

    private void PuzzleCompleted()
    {
        Destroy(blockedArea);
    }

    private int GetNumberOfAnsweredQuizzes()
    {
        return answerQuizzes;
    }

    private void OnEnable()
    {
        PuzzleManager.PuzzleIsComplete += PuzzleCompleted;
        Quiz.QuizComplete += QuizCompleted;
        PuzzleManager.quizzesAnswered += GetNumberOfAnsweredQuizzes;
    }

    private void OnDisable()
    {
        PuzzleManager.PuzzleIsComplete -= PuzzleCompleted;
        Quiz.QuizComplete -= QuizCompleted;
        PuzzleManager.quizzesAnswered -= GetNumberOfAnsweredQuizzes;
    }
}
