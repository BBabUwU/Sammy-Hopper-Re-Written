using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stg3_Quiz : MonoBehaviour
{
    //Variables
    private string userInput1;
    private string userInput2;
    [HideInInspector] public int questionIndex;
    private string questionAnswer1;
    private string questionAnswer2;
    public Stg3_QuestionBank questionBank;
    public GameState returnGamestate;

    private void OnEnable()
    {
        //Events
        InputFieldController.onValueChangeInput += ReadUserInput;
        InputFieldController.onSubmitAnswer += CheckIfCorrect;

        Actions.NoAnswer += NoSolution;
        Actions.correctGraph += Correct;

        RandomizeQuestion();
    }

    private void RandomizeQuestion()
    {
        questionIndex = UnityEngine.Random.Range(0, questionBank.Qbank.Count);

        while (questionBank.Qbank[questionIndex].notActive)
        {
            questionIndex = UnityEngine.Random.Range(0, questionBank.Qbank.Count);
        }

        questionAnswer1 = questionBank.Qbank[questionIndex].answer1;
        questionAnswer2 = questionBank.Qbank[questionIndex].answer2;
        questionBank.Qbank[questionIndex].notActive = true;
        DisplayQuestion();

        Debug.Log("Answer 1: " + questionBank.Qbank[questionIndex].answer1);
        Debug.Log("Answer 2: " + questionBank.Qbank[questionIndex].answer2);
    }

    private void DisplayQuestion()
    {
        Actions.UpdateQuestionText?.Invoke(UITextType.QuestionText, questionBank.Qbank[questionIndex].question);
    }
    public void ReadUserInput(AnswerType _type, string _answer)
    {
        if (_type == AnswerType.Answer1) userInput1 = _answer;
        if (_type == AnswerType.Answer2) userInput2 = _answer;
    }

    private bool IsCorrect()
    {
        bool correctAnswer;

        if (questionBank.Qbank[questionIndex].noSolution)
        {
            return false;
        }

        correctAnswer = userInput1 == questionAnswer1 && userInput2 == questionAnswer2 ? true : false;

        return correctAnswer;
    }

    private void NoSolution()
    {
        if (questionBank.Qbank[questionIndex].noSolution)
        {
            if (IsGraphing())
            {
                CheckGraph();
            }
            else
            {
                Correct();
            }
        }
        else
        {
            Incorrect();
        }
    }

    private void Correct()
    {
        Actions.correctAnswer?.Invoke();
        ReturnWorld();
        this.enabled = false;
    }

    private void Incorrect()
    {
        Actions.inCorrect?.Invoke();
        ReturnWorld();
        this.enabled = false;
    }

    private void ReturnWorld()
    {
        Actions.ClearInput?.Invoke();
        UIManager.Instance.TurnOffUI(UIType.QuizUI);
        UIManager.Instance.TurnOffUI(UIType.GraphingUI);
        GameManager.Instance.UpdateGameState(returnGamestate);
    }

    private void CheckIfCorrect()
    {
        if (IsCorrect())
        {
            if (IsGraphing())
            {
                CheckGraph();
            }
            else
            {
                Correct();
            }
        }

        else
        {
            Incorrect();
        }

        Actions.ClearInput?.Invoke();
    }

    private void CheckGraph()
    {
        UIManager.Instance.TurnOnUI(UIType.GraphingUI);
    }

    private bool IsGraphing()
    {
        return questionBank.Qbank[questionIndex].topic == LinearTopic.Graphing;
    }

    private void OnDisable()
    {
        InputFieldController.onValueChangeInput -= ReadUserInput;
        InputFieldController.onSubmitAnswer -= CheckIfCorrect;

        Actions.NoAnswer -= NoSolution;
        Actions.correctGraph -= Correct;
    }
}
