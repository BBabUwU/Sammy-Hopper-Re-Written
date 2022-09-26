using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class BossManager : MonoBehaviour
{
    //Used to instantiate/Create essence gameObjects
    [SerializeField] private GameObject essencePrefab;

    //Gets the transform where the essence and the boss will drop
    [SerializeField] private List<Transform> dropPoints = new List<Transform>();

    //Essence left
    private int essenseCount;

    //Delegates
    //Gets questions from the BossQuestionBank
    public static event Func<List<Chapter1QnATemplate>> questionBank;
    public static event Action<int> damageBoss;
    public static event Action destroyEssence;
    public static event Action<TimerType> startTimer;

    private BossQuiz quiz;

    private void Awake()
    {
        quiz = GetComponent<BossQuiz>();
    }

    private void Start()
    {
        DropEssence();
    }

    private void DropEssence()
    {
        //startTimer?.Invoke(TimerType.collectTimer);
        essenseCount = 0;

        for (int i = 0; i < dropPoints.Count; i++)
        {
            Instantiate(essencePrefab, dropPoints[i].position, Quaternion.identity);
        }
    }

    private void EssensePicked()
    {
        essenseCount++;

        if (essenseCount == 5)
        {
            StartCoroutine(StartQuiz());
        }
    }

    IEnumerator StartQuiz()
    {
        quiz.enabled = true;
        quiz.quizBank = questionBank();
        quiz.numberOfQuestions = essenseCount;
        GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        quiz.RandomizeQuestion();

        yield return new WaitForSeconds(1f);
        //startTimer?.Invoke(TimerType.answerTimer);
    }

    private void QuizCompleted(int score)
    {
        damageBoss?.Invoke(score);

        DropEssence();

        //Debug.Log("Boss attack");
    }

    private void TimesUp(TimerType timerType)
    {
        destroyEssence?.Invoke();

        if (timerType == TimerType.collectTimer)
        {
            if (essenseCount > 0)
            {
                StartCoroutine(StartQuiz());
            }

            else
            {
                Debug.Log("Boss attack");
            }

        }

        else if (timerType == TimerType.answerTimer)
        {
            if (quiz.enabled) quiz.StopQuiz();
            Debug.Log("Here");
        }
    }

    private void OnEnable()
    {
        Timer.timesUp += TimesUp;

        PickEssence.EssenseCount += EssensePicked;

        BossQuiz.score += QuizCompleted;

        Timer.timesUp += TimesUp;
    }

    private void OnDisable()
    {
        Timer.timesUp -= TimesUp;

        PickEssence.EssenseCount -= EssensePicked;

        BossQuiz.score -= QuizCompleted;

        Timer.timesUp -= TimesUp;
    }
}
