using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stg3_QuizManager : MonoBehaviour
{
    [SerializeField] private GameObject blockedPath;
    private Stg3_Quiz quiz;
    private ShootProj attacker;
    [SerializeField] private int maxScore;
    private int quizCounter = 0;

    private void Awake()
    {
        quiz = GetComponent<Stg3_Quiz>();
        attacker = GetComponent<ShootProj>();
    }

    private void StartQuiz()
    {
        attacker.parryCounter++;

        if (attacker.parryCounter == 3)
        {
            GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
            quiz.enabled = true;
        }
    }

    private void QuizOver()
    {
        if (quizCounter == maxScore)
        {
            attacker.enabled = false;
            Destroy(blockedPath);
        }
    }

    private void ResumeParry()
    {
        QuizOver();
        attacker.parryCounter = 0;

    }

    private void IncreaseQuizCounter()
    {
        quizCounter++;
    }

    private void OnEnable()
    {
        Actions.parried += StartQuiz;
        Actions.correctAnswer += IncreaseQuizCounter;
        Actions.resumeParry += ResumeParry;

        attacker.enabled = true;
        Actions.disableParry?.Invoke(false);

    }

    private void OnDisable()
    {
        Actions.parried -= StartQuiz;
        Actions.correctAnswer -= IncreaseQuizCounter;
        Actions.resumeParry -= ResumeParry;
    }
}
