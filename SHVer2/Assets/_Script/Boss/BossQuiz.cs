using System.Collections.Generic;
using UnityEngine;
using System;


public class BossQuiz : MonoBehaviour
{
    //Variables
    private string userInput1;
    private string userInput2;
    private int questionIndex;
    private string questionAnswer1;
    private string questionAnswer2;
    public int numberOfQuestions;
    private int currentQuestionIndex = 1;
    private int currentScore;

    //Delegates
    //UI Delegate
    public static event Action<UITextType, string> UpdateQuestionText;
    public static event Action ClearInput;
    public static event Action<int> score;

    [NonReorderable]
    public List<Chapter1QnATemplate> quizBank;


    private void OnEnable()
    {
        //Inputs
        InputFieldController.onValueChangeInput += ReadUserInput;
        InputFieldController.onSubmitAnswer += CheckIfCorrect;

    }

    public void RandomizeQuestion()
    {
        questionIndex = UnityEngine.Random.Range(0, quizBank.Count);

        while (quizBank[questionIndex].NotActive)
        {
            questionIndex = UnityEngine.Random.Range(0, quizBank.Count);
        }

        questionAnswer1 = quizBank[questionIndex].correctAnswer1;
        questionAnswer2 = quizBank[questionIndex].correctAnswer2;
        quizBank[questionIndex].NotActive = true;
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        UpdateQuestionText?.Invoke(UITextType.QuestionText, quizBank[questionIndex].question);
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
            currentScore++;
        }
        else
        {
            quizBank[questionIndex].NotActive = false;
        }

        CheckIfComplete();
    }

    private void CheckIfComplete()
    {

        ClearInput?.Invoke();

        if (currentQuestionIndex < numberOfQuestions)
        {
            RandomizeQuestion();
            currentQuestionIndex++;
        }
        else
        {
            StopQuiz();
        }
    }

    public void StopQuiz()
    {
        score?.Invoke(currentScore);
        currentQuestionIndex = 1;
        currentScore = 0;
        UIManager.Instance.TurnOffUI(UIType.QuizUI);
        GameManager.Instance.UpdateGameState(GameState.BossBattle);
    }

    private void OnDisable()
    {
        //Input
        InputFieldController.onValueChangeInput -= ReadUserInput;
        InputFieldController.onSubmitAnswer -= CheckIfCorrect;
    }
}