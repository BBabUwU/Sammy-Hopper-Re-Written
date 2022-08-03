using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizScript : MonoBehaviour
{
    [SerializeField] private GameObject practicalProblemsObj;
    [SerializeField] private TMP_Text questionTextUI;
    [SerializeField] private TMP_InputField answerFieldUI_1;
    [SerializeField] private TMP_InputField answerFieldUI_2;

    //NonReorderable attribute fix the visual bug.
    //List of questions and answers.
    [NonReorderable]
    [SerializeField]
    private List<Chapter1QnATemplate> questionAndAnswers = new List<Chapter1QnATemplate>();
    [SerializeField] private int maximumQuestions;
    private int currentQuestionIndex;
    private int currentQuestionNumber = 1;

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
    private bool isPassed;

    [ExecuteInEditMode]
    void OnValidate()
    {
        //Limits the number of questions allowed to be edited on the editor.
        //If the editor changes the value of maximum questions higher than the number of questions in the list, it will set the maximum questions to the number of questions of the list.
        if (maximumQuestions > questionAndAnswers.Count)
        {
            maximumQuestions = questionAndAnswers.Count;
        }
    }

    //-----------------------------BOSS SPECIFIC VARIABLES--------------------------------------

    private void Start()
    {
        practicalProblemsObj.SetActive(true);
        //Passing score will be half of the maximum number of questions, Mathf.Abs will remove the decimal.
        passingScore = (Mathf.Abs(maximumQuestions / 2));
        Debug.Log("Passing score: " + passingScore);
        RandomizeQuestion();
        DisplayCurrentQuestion();
    }

    private void Update()
    {
        //Allows the player to submit answer using the return (enter) button.
        PlayerSubmitsAnswer();
    }

    private void RandomizeQuestion()
    {
        currentQuestionIndex = Random.Range(0, questionAndAnswers.Count);
        currentAnswer1 = questionAndAnswers[currentQuestionIndex].correctAnswer1;
        currentAnswer2 = questionAndAnswers[currentQuestionIndex].correctAnswer2;
    }

    private void DisplayCurrentQuestion()
    {
        questionTextUI.text = questionAndAnswers[currentQuestionIndex].question;
    }

    public void ReadUserInput_1(string answer)
    {
        userInput1 = answer;
    }

    public void ReadUserInput_2(string answer)
    {
        userInput2 = answer;
    }

    private void PlayerSubmitsAnswer()
    {
        //Checks if player is allowed to click enter, length of text is greater than 0 and return key (enter key) or numpad enter key is pressed.
        if ((allowEnter && (answerFieldUI_1.text.Length > 0) || (answerFieldUI_2.text.Length > 0)) && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            CheckIfCorrect();
            allowEnter = false;
        }
        else
        {
            allowEnter = answerFieldUI_1.isFocused || answerFieldUI_2.isFocused;
        }
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

    private void ClearInputField()
    {
        answerFieldUI_1.text = "";
        answerFieldUI_2.text = "";
        answerFieldUI_1.Select();
    }

    private void NextQuestion()
    {
        questionAndAnswers.RemoveAt(currentQuestionIndex);
        RandomizeQuestion();
        DisplayCurrentQuestion();
    }

    private void CheckIfPassed()
    {
        if (currentScore >= passingScore)
        {
            isPassed = true;
            Debug.Log("Has passed");
        }
        else
        {
            isPassed = false;
            Debug.Log("Has failed");
        }
    }

    //Check answer function should be in the answefield "on-end enter"
    private void CheckIfCorrect()
    {
        if (IsCorrectAnswer())
        {
            currentScore++;
            currentQuestionNumber++;
            ClearInputField();

            if (NoMoreQuestions())
            {
                practicalProblemsObj.SetActive(false);
                CheckIfPassed();
            }

            else
            {
                NextQuestion();
            }
        }
        else
        {
            //Damage player here
            Debug.Log("Player damaged");
        }
    }

    public bool PlayerHasPassedQuiz()
    {
        return isPassed;
    }
}
