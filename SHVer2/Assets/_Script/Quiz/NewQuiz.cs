using System.Collections.Generic;
using UnityEngine;
using System;

public class NewQuiz : MonoBehaviour
{
    private string answer_1;
    private string answer_2;
    public bool quizStarted;

    //Events
    public static Action<UITextType, string> updateQuestionText;

    private void Start()
    {
        GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        UIManager.Instance.TurnOnUI(UIType.QuizUI);
    }

    private void OnEnable()
    {
        //Events
        InputController.onValueChangedInput += ReadUserInput;
    }

    public void ReadUserInput(AnswerType _type, string _answer)
    {
        if (_type == AnswerType.Answer1) answer_1 = _answer;
        if (_type == AnswerType.Answer2) answer_2 = _answer;
    }

    private void OnDisable()
    {
        //Events
        InputController.onValueChangedInput -= ReadUserInput;
    }
}
