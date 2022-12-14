using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stg3_QuizManager : MonoBehaviour
{
    [SerializeField] private GameObject entrancePath;
    [SerializeField] private GameObject exitPath;
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
            attacker.StopAllCoroutines();
            attacker.enabled = false;
            GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
            quiz.enabled = true;
        }
    }

    private void QuizOver()
    {
        if (quizCounter == maxScore)
        {
            attacker.enabled = false;
            Destroy(exitPath);
            this.enabled = false;
        }
    }

    private void ResumeParry()
    {
        attacker.enabled = true;
        attacker.canShoot = true;
        attacker.parryCounter = 0;
        QuizOver();
    }

    private void IncreaseQuizCounter()
    {
        quizCounter++;
        ResumeParry();
    }

    private void OnEnable()
    {
        Actions.parried += StartQuiz;
        Actions.correctAnswer += IncreaseQuizCounter;
        Actions.resumeParry += ResumeParry;

        attacker.enabled = true;
        entrancePath.SetActive(true);
        Actions.disableParry?.Invoke(false);
    }

    private void OnDisable()
    {
        Actions.parried -= StartQuiz;
        Actions.correctAnswer -= IncreaseQuizCounter;
        Actions.resumeParry -= ResumeParry;
    }
}
