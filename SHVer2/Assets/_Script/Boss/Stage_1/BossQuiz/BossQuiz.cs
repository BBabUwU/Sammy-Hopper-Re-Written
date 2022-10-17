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

    //Quiz Difficulty
    public QuizDiff difficulty;

    //Delegates
    //UI Delegate
    public static event Action<UITextType, string> UpdateQuestionText;
    public static event Action ClearInput;

    //Send message when the player choose the right answer
    public static event Action<QuizDiff> correctAnswer;

    //Destroy current essence
    public static event Action<QuizDiff> destroyEssence;

    //Components
    private BossQuestionBank questionBank;

    private void Awake()
    {
        questionBank = GetComponent<BossQuestionBank>();
    }

    public void RandomizeQuestion()
    {
        SetInactive();
        questionIndex = UnityEngine.Random.Range(0, questionBank.QnA.Count);

        //While not active and not equal to the right difficulty
        while (questionBank.QnA[questionIndex].NotActive || difficulty != questionBank.QnA[questionIndex].difficulty)
        {
            questionIndex = UnityEngine.Random.Range(0, questionBank.QnA.Count);
        }

        questionAnswer1 = questionBank.QnA[questionIndex].correctAnswer1;
        questionAnswer2 = questionBank.QnA[questionIndex].correctAnswer2;
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        UpdateQuestionText?.Invoke(UITextType.QuestionText_1, questionBank.QnA[questionIndex].question);

        Debug.Log("-------------------------------------");
        Debug.Log("Correct Answer 1: " + questionAnswer1);
        Debug.Log("Correct Answer 2: " + questionAnswer2);
    }

    public void ReadUserInput(AnswerType _type, string _answer)
    {
        if (_type == AnswerType.Answer1) userInput1 = _answer;
        else if (_type == AnswerType.Answer2) userInput2 = _answer;
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
            questionBank.QnA[questionIndex].NotActive = true;
            destroyEssence?.Invoke(difficulty);
            correctAnswer?.Invoke(difficulty);
            ClearInput?.Invoke();
            GameManager.Instance.UpdateGameState(GameState.BossBattle);
            UIManager.Instance.TurnOffUI(UIType.QuizUI);
            enabled = false;
        }
        else
        {
            WrongAnswer();
        }
    }

    private void WrongAnswer()
    {
        questionBank.QnA[questionIndex].NotActive = false;
        destroyEssence?.Invoke(difficulty);
        Actions.wrongAnswer?.Invoke(difficulty);
        ClearInput?.Invoke();
        GameManager.Instance.UpdateGameState(GameState.BossBattle);
        UIManager.Instance.TurnOffUI(UIType.QuizUI);
        enabled = false;
    }

    public void StopQuiz()
    {
        questionBank.QnA[questionIndex].NotActive = false;
        ClearInput?.Invoke();
        UIManager.Instance.TurnOffUI(UIType.QuizUI);
        GameManager.Instance.UpdateGameState(GameState.BossBattle);
    }

    ///<summary>
    ///Inactive check
    ///</summary>

    private void SetInactive()
    {
        if (AllEasyInactive())
        {
            foreach (var qna in questionBank.QnA)
            {
                if (qna.difficulty == QuizDiff.Easy) qna.NotActive = false;
            }
        }
        else if (AllHardInactive())
        {
            foreach (var qna in questionBank.QnA)
            {
                if (qna.difficulty == QuizDiff.Hard) qna.NotActive = false;
            }
        }
        else
        {
            Debug.Log("There still left");
        }
    }

    private bool AllEasyInactive()
    {
        bool allInactive = true;

        for (int i = 0; i < questionBank.QnA.Count; i++)
        {
            if (questionBank.QnA[i].difficulty == QuizDiff.Easy)
            {
                if (questionBank.QnA[i].NotActive == false)
                {
                    allInactive = false;
                }
            }
        }

        return allInactive;
    }

    private bool AllHardInactive()
    {
        bool allInactive = true;

        for (int i = 0; i < questionBank.QnA.Count; i++)
        {
            if (questionBank.QnA[i].difficulty == QuizDiff.Hard)
            {
                if (questionBank.QnA[i].NotActive == false)
                {
                    allInactive = false;
                }
            }
        }

        return allInactive;
    }

    private void OnEnable()
    {
        //Inputs
        InputFieldController.onValueChangeInput += ReadUserInput;
        InputFieldController.onSubmitAnswer += CheckIfCorrect;

        Actions.leaveQuiz += WrongAnswer;
    }

    private void OnDisable()
    {
        //Input
        InputFieldController.onValueChangeInput -= ReadUserInput;
        InputFieldController.onSubmitAnswer -= CheckIfCorrect;

        Actions.leaveQuiz -= WrongAnswer;
    }
}