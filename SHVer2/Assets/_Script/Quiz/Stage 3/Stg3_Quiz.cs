using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stg3_Quiz : MonoBehaviour
{
    //Variables
    [SerializeField] private bool isBoss;
    private string userInput1;
    private string userInput2;
    [HideInInspector] public int questionIndex;
    public string questionAnswer1;
    public string questionAnswer2;
    public int[] intialPoint_1_answer = new int[2];
    public int[] intialPoint_2_answer = new int[2];
    public Stg3_QuestionBank questionBank;
    public GameState returnGamestate;
    public GameObject graphingDisplay;

    private void OnEnable()
    {
        //Events
        InputFieldController.onValueChangeInput += ReadUserInput;
        InputFieldController.onSubmitAnswer += CheckIfCorrect;

        Actions.NoAnswer += NoSolution;
        Actions.correctGraph += Correct;
        Actions.currentQuiz += GetQuiz;

        graphingDisplay.SetActive(true);
        RandomizeQuestion();
    }

    private void RandomizeQuestion()
    {
        if (Check_All_NotActive()) Reset_Active();

        questionIndex = UnityEngine.Random.Range(0, questionBank.Qbank.Count);

        while (questionBank.Qbank[questionIndex].notActive)
        {
            questionIndex = UnityEngine.Random.Range(0, questionBank.Qbank.Count);
        }

        Debug.Log(questionBank.Qbank[questionIndex].topic);

        questionAnswer1 = questionBank.Qbank[questionIndex].answer1;
        questionAnswer2 = questionBank.Qbank[questionIndex].answer2;

        if (IsGraphing())
        {
            intialPoint_1_answer[0] = questionBank.Qbank[questionIndex].first_initialPoint[0];
            intialPoint_1_answer[1] = questionBank.Qbank[questionIndex].first_initialPoint[1];

            intialPoint_2_answer[0] = questionBank.Qbank[questionIndex].second_initialPoint[0];
            intialPoint_2_answer[1] = questionBank.Qbank[questionIndex].second_initialPoint[1];

            Debug.Log("Initial Point - First Line: " + intialPoint_1_answer[0] + "," + intialPoint_1_answer[1]);

            Debug.Log("Initial Point - Second Line: " + intialPoint_2_answer[0] + "," + intialPoint_2_answer[1]);
        }

        questionBank.Qbank[questionIndex].notActive = true;

        Display_Topic_Text();
        DisplayQuestion();

        Debug.Log("Answer 1: " + questionBank.Qbank[questionIndex].answer1);
        Debug.Log("Answer 2: " + questionBank.Qbank[questionIndex].answer2);
    }

    private void DisplayQuestion()
    {
        if (questionBank.Qbank[questionIndex].topic == LinearTopic.Graphing)
        {
            GameManager.Instance.UpdateGameState(GameState.AnsweringGraph);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        }

        Actions.UpdateQuestionText?.Invoke(UITextType.QuestionText_1, questionBank.Qbank[questionIndex].question_1);

        Actions.UpdateQuestionText?.Invoke(UITextType.QuestionText_2, questionBank.Qbank[questionIndex].question_2);
    }

    private void Display_Topic_Text()
    {
        if (questionBank.Qbank[questionIndex].topic == LinearTopic.Graphing)
        {
            Actions.UpdateTopic?.Invoke("Graphing");
        }
        else if (questionBank.Qbank[questionIndex].topic == LinearTopic.Elimination)
        {
            Actions.UpdateTopic?.Invoke("Elimination");
        }
        else if (questionBank.Qbank[questionIndex].topic == LinearTopic.Substitution)
        {
            Actions.UpdateTopic?.Invoke("Substitution");
        }
        else if (questionBank.Qbank[questionIndex].topic == LinearTopic.Determinants)
        {
            Actions.UpdateTopic?.Invoke("Determinants");
        }
    }

    public void ReadUserInput(AnswerType _type, string _answer)
    {

        if (_type == AnswerType.Answer1) userInput1 = _answer;
        if (_type == AnswerType.Answer2) userInput2 = _answer;
    }

    private void NoSolution()
    {
        if (questionBank.Qbank[questionIndex].noSolution)
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    private void Correct()
    {
        Debug.Log("Correct");
        Actions.correctAnswer?.Invoke();
        ReturnWorld();
        this.enabled = false;
    }

    private void Incorrect()
    {
        Debug.Log("Wrong");
        Actions.inCorrect?.Invoke();
        questionBank.Qbank[questionIndex].notActive = false;
        ReturnWorld();
        this.enabled = false;
    }

    private bool IsCorrect()
    {
        bool correctAnswer;

        if (IsGraphing())
        {
            if (Actions.CheckGraph())
            {
                correctAnswer = userInput1 == questionAnswer1 && userInput2 == questionAnswer2 ? true : false;
                return correctAnswer;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (questionBank.Qbank[questionIndex].noSolution)
            {
                return false;
            }
        }

        correctAnswer = userInput1 == questionAnswer1 && userInput2 == questionAnswer2 ? true : false;

        return correctAnswer;
    }

    private bool Check_All_NotActive()
    {
        bool allNotActive = false;

        foreach (var question in questionBank.Qbank)
        {
            if (question.notActive == true)
            {
                allNotActive = true;
            }
            else
            {
                allNotActive = false;
                break;
            }
        }

        return allNotActive;
    }

    private void Reset_Active()
    {
        foreach (var question in questionBank.Qbank)
        {
            question.notActive = false;
        }
    }

    private void ReturnWorld()
    {
        Actions.ClearInput?.Invoke();
        UIManager.Instance.TurnOffUI(UIType.QuizUI);
        Actions.Switch_Camera?.Invoke(CameraType.PlayerCamera);
        Actions.Set_DefaultCamera?.Invoke(CameraType.PlayerCamera);

        GameManager.Instance.UpdateGameState(returnGamestate);

        if (!isBoss) Actions.resumeParry?.Invoke();
    }

    private void CheckIfCorrect()
    {
        if (IsCorrect())
        {
            Correct();
        }

        else
        {
            Incorrect();
        }

        Actions.ClearInput?.Invoke();
    }

    private bool IsGraphing()
    {
        return questionBank.Qbank[questionIndex].topic == LinearTopic.Graphing;
    }

    private Stg3_Quiz GetQuiz()
    {
        return this;
    }

    private void OnDisable()
    {
        InputFieldController.onValueChangeInput -= ReadUserInput;
        InputFieldController.onSubmitAnswer -= CheckIfCorrect;

        Actions.NoAnswer -= NoSolution;
        Actions.correctGraph -= Correct;
        Actions.currentQuiz -= GetQuiz;

        graphingDisplay.SetActive(false);
    }
}
