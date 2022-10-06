using System.Collections.Generic;
using UnityEngine;
using System;


public class ExplorationQuiz : MonoBehaviour
{
    //Variables
    private string userInput1;
    private string userInput2;
    private int questionIndex;
    private string questionAnswer1;
    private string questionAnswer2;

    [SerializeField] private int QuizPart;
    [SerializeField] private QuizQuestionBank questionBank;

    //Delegate
    //UI Delegate
    public static event Action<UITextType, string> UpdateQuestionText;
    public static event Action ClearInput;

    //Sends message that the quiz is complete to the puzzle manager
    public static event Action<int> QuizComplete;

    private void Start()
    {
        RandomizeQuestion();
    }

    private void OnEnable()
    {
        DisplayQuestion();

        //Events
        InputFieldController.onValueChangeInput += ReadUserInput;
        InputFieldController.onSubmitAnswer += CheckIfCorrect;
    }

    private void RandomizeQuestion()
    {
        questionIndex = UnityEngine.Random.Range(0, questionBank.qna.Count);

        while (questionBank.qna[questionIndex].NotActive)
        {
            questionIndex = UnityEngine.Random.Range(0, questionBank.qna.Count);
        }

        questionAnswer1 = questionBank.qna[questionIndex].correctAnswer1;
        questionAnswer2 = questionBank.qna[questionIndex].correctAnswer2;
        questionBank.qna[questionIndex].NotActive = true;
        DisplayQuestion();

        Debug.Log("Answer 1: " + questionBank.qna[questionIndex].correctAnswer1);
        Debug.Log("Answer 2: " + questionBank.qna[questionIndex].correctAnswer2);
    }

    private void DisplayQuestion()
    {
        UpdateQuestionText?.Invoke(UITextType.QuestionText, questionBank.qna[questionIndex].question);
    }
    public void ReadUserInput(AnswerType _type, string _answer)
    {
        if (_type == AnswerType.Answer1) userInput1 = _answer;
        if (_type == AnswerType.Answer2) userInput2 = _answer;
    }

    private bool IsCorrect()
    {
        bool correctAnswer = userInput1 == questionAnswer1 && userInput2 == questionAnswer2 ? true : false;
        return correctAnswer;
    }

    private void CheckIfCorrect()
    {
        if (IsCorrect())
        {
            ClearInput?.Invoke();
            UIManager.Instance.TurnOffUI(UIType.QuizUI);
            GameManager.Instance.UpdateGameState(GameState.Exploration);
            QuizComplete?.Invoke(QuizPart);
            Destroy(gameObject);
        }

        else
        {
            Debug.Log("Wrong");
        }

        ClearInput?.Invoke();
    }

    private void OnDisable()
    {
        InputFieldController.onValueChangeInput -= ReadUserInput;
        InputFieldController.onSubmitAnswer -= CheckIfCorrect;
    }

}