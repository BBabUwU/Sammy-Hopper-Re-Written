using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerTracker : MonoBehaviour
{
    [SerializeField] private int quizPart;
    [HideInInspector] public int answeredQuestions = 0;

    private void IncrementQuiz(int quizPart)
    {
        if (this.quizPart == quizPart)
        {
            answeredQuestions++;
        }
    }

    private void OnEnable()
    {
        ExplorationQuiz.QuizComplete += IncrementQuiz;
    }

    private void OnDisable()
    {
        ExplorationQuiz.QuizComplete -= IncrementQuiz;
    }
}
