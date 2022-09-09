using System.Collections.Generic;
using UnityEngine;
using System;

public class QuizScript : MonoBehaviour
{
    public int quizID;
    public QuizType quizType;

    //NonReorderable attribute fix the visual bug.
    //List of questions and answers.
    [NonReorderable]
    [SerializeField]
    private List<Chapter1QnATemplate> intialQuestionAndAnswers;
    private List<Chapter1QnATemplate> questionAndAnswers;

    [SerializeField] private int maximumQuestions;
    private int currentQuestionIndex;
    private int currentQuestionNumber = 1;
    [SerializeField] private bool allowMistakes = true;
    [SerializeField] private float playerDamage = 10f;

    //PLayer health script to damage player
    private PlayerHealth _playerHealth;

    //Stores correct answers.
    private string currentAnswer1;
    private string currentAnswer2;

    //For the base number.
    private string userInput1;
    //For the decimal point
    private string userInput2;

    //Checks if the player is allowed to submit answer.
    private bool allowEnter;
    private int passingScore;
    private int currentScore = 0;
    public bool quizStarted;
    private bool isPassed;

    //Timer Variables
    [SerializeField] private bool EnableTimer = false;
    [SerializeField] private float timeLeft;

    //Used to reset the time
    private float defaultTimeLeft;

    //Events
    //Record quiz events
    public static event Action<Quiz> AddQuiz;
    public static event Action<Quiz> QuizPassed;
    public Quiz quiz;

    //UI Events
    public static event Action<UITextType, string> UpdateQuestionText;
    public static event Action<UITextType, string> UpdateTimerText;
    public static event Action ClearInput;

    [ExecuteInEditMode]
    void OnValidate()
    {
        //Limits the number of questions allowed to be edited on the editor.
        //If the editor changes the value of maximum questions higher than the number of questions in the list, it will set the maximum questions to the number of questions of the list.
        if (maximumQuestions > intialQuestionAndAnswers.Count)
        {
            maximumQuestions = intialQuestionAndAnswers.Count;
        }
    }

    private void Awake()
    {
        InitializeQuizDetails();

        this.enabled = false;
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        quizStarted = true;
    }

    private void OnEnable()
    {
        questionAndAnswers = intialQuestionAndAnswers.GetRange(0, intialQuestionAndAnswers.Count);
        //Events
        InputFieldController.onValueChangedInput += ReadUserInput;
        InputFieldController.onSubmitAnswer += CheckIfCorrect;

        defaultTimeLeft = timeLeft;
        //Converts timer to seconds
        timeLeft = timeLeft * 60;
        //Changes the state of the game to answering quiz
        GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        //Passing score will be half of the maximum number of questions, Mathf.Abs will remove the decimal. If only one question, passing score will be one.
        SetPassingScore();
        RandomizeQuestion();
        DisplayCurrentQuestion();
    }

    private void OnDisable()
    {
        //Events
        InputFieldController.onValueChangedInput -= ReadUserInput;
        InputFieldController.onSubmitAnswer -= CheckIfCorrect;
    }

    private void Update()
    {
        //Timer will run if enabled
        Timer();
    }

    private void InitializeQuizDetails()
    {
        quiz.quizID = quizID;
        quiz.quizType = quizType;
        quiz.isPassed = isPassed;
        quiz.score = currentScore;
        quiz.totalScore = maximumQuestions;
        AddQuiz?.Invoke(quiz);
    }

    private void SetQuizValues()
    {
        quiz.isPassed = isPassed;
        quiz.score = currentScore;
    }

    private void SetPassingScore()
    {
        if (maximumQuestions == 1)
        {
            passingScore = 1;
        }
        else
        {
            passingScore = (Mathf.Abs(maximumQuestions / 2));
        }
    }

    private void ResetValues()
    {
        timeLeft = defaultTimeLeft;
        currentQuestionNumber = 1;
        currentQuestionIndex = 0;
        currentScore = 0;
        questionAndAnswers = intialQuestionAndAnswers.GetRange(0, intialQuestionAndAnswers.Count);
    }

    //------------------------------------------------Timer---------------------------------------
    private void Timer()
    {
        if (EnableTimer)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                CheckIfPassed();
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float _minutes = Mathf.FloorToInt(currentTime / 60);
        float _seconds = Mathf.FloorToInt(currentTime % 60);

        string timerString = string.Format("{0:00} : {1:00}", _minutes, _seconds);

        UpdateTimerText?.Invoke(UITextType.TimeText, timerString);
    }

    //--------------------------------------------------------------------------------------------
    private void RandomizeQuestion()
    {
        currentQuestionIndex = UnityEngine.Random.Range(0, questionAndAnswers.Count);
        currentAnswer1 = questionAndAnswers[currentQuestionIndex].correctAnswer1;
        currentAnswer2 = questionAndAnswers[currentQuestionIndex].correctAnswer2;
    }

    private void DisplayCurrentQuestion()
    {
        UpdateQuestionText?.Invoke(UITextType.QuestionText, questionAndAnswers[currentQuestionIndex].question);
    }

    public void ReadUserInput(AnswerType _type, string _answer)
    {
        if (_type == AnswerType.Answer1) userInput1 = _answer;
        if (_type == AnswerType.Answer2) userInput2 = _answer;
    }


    //Check functions
    private bool IsCorrectAnswer()
    {
        bool correctAnswer = userInput1 == currentAnswer1 && userInput2 == currentAnswer2 ? true : false;
        return correctAnswer;
    }

    private bool NoMoreQuestions()
    {

        bool noMoreQuestions = 1 == questionAndAnswers.Count || currentQuestionNumber > maximumQuestions ? true : false;
        return noMoreQuestions;
    }

    private void NextQuestion()
    {
        questionAndAnswers.RemoveAt(currentQuestionIndex);
        RandomizeQuestion();
        DisplayCurrentQuestion();
    }

    //Check answer function should be in the answefield "on-end enter"
    private void CheckIfCorrect()
    {
        if (IsCorrectAnswer())
        {
            currentScore++;
            currentQuestionNumber++;
            ClearInput?.Invoke();
        }
        else if (!allowMistakes)
        {
            //Damage player when the answer is wrong.
            //It will only damage the player when allow mistakes is disabled.
            _playerHealth.DamagePlayer(playerDamage);
        }

        if (NoMoreQuestions())
        {
            CheckIfPassed();
        }

        else
        {
            NextQuestion();
        }
    }

    private void CheckIfPassed()
    {
        if (currentScore >= passingScore)
        {
            isPassed = true;
            SetQuizValues();
            QuizPassed?.Invoke(quiz);
        }
        else
        {
            isPassed = false;
            SetQuizValues();
            ResetValues();
        }

        SetState();
        UIManager.Instance.TurnOffUI(UIType.QuizUI);
        ClearInput?.Invoke();
        quizStarted = false;
        this.enabled = false;
    }

    private void SetState()
    {
        if (quizType == QuizType.ExplorationQuiz) GameManager.Instance.UpdateGameState(GameState.Exploration);

        else if (quizType == QuizType.BossQuiz) GameManager.Instance.UpdateGameState(GameState.BossQuizEvaluation);
    }
}
