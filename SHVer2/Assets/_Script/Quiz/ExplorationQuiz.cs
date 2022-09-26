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

    //Delegates
    //QnA Delegate
    //Get list of questions from QuizQuestionBank script
    public static Func<List<Chapter1QnATemplate>> QuestionBank;

    //UI Delegate
    public static event Action<UITextType, string> UpdateQuestionText;
    public static event Action ClearInput;

    //Sends message that the quiz is complete to the puzzle manager
    public static event Action QuizComplete;

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
        questionIndex = UnityEngine.Random.Range(0, QuestionBank().Count);

        while (QuestionBank()[questionIndex].NotActive)
        {
            questionIndex = UnityEngine.Random.Range(0, QuestionBank().Count);
        }

        questionAnswer1 = QuestionBank()[questionIndex].correctAnswer1;
        questionAnswer2 = QuestionBank()[questionIndex].correctAnswer2;
        QuestionBank()[questionIndex].NotActive = true;
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        UpdateQuestionText?.Invoke(UITextType.QuestionText, QuestionBank()[questionIndex].question);
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
            QuizComplete?.Invoke();
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