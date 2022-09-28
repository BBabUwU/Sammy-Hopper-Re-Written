using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    private BossQuiz quiz;

    private void Awake()
    {
        quiz = GetComponent<BossQuiz>();
        quiz.enabled = false;
    }

    private void StartQuiz(QuizDiff diff)
    {
        quiz.difficulty = diff;
        quiz.enabled = true;
        GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        UIManager.Instance.TurnOnUI(UIType.QuizUI);
        quiz.RandomizeQuestion();
    }

    private void StopQuiz()
    {
        quiz.StopQuiz();
    }

    private void OnEnable()
    {
        StartableBossQuiz.StartQuiz += StartQuiz;
        BossManager.stopQuiz += StopQuiz;
    }

    private void OnDisable()
    {
        StartableBossQuiz.StartQuiz -= StartQuiz;
        BossManager.stopQuiz -= StopQuiz;
    }
}
