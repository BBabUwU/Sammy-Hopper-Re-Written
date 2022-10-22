using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Stage2_Quiz : MonoBehaviour
{
    [SerializeField] private Stg2_QuestionBank Qbank;
    [SerializeField] private List<BoxCollider2D> interactCollider;
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private int numberOfQuestions;
    [SerializeField] private GameObject blockedPath;
    private int questionCounter = 0;
    public int quizNumber;
    private string currentAnswer;
    private int questionIndex;
    private List<int> randomChoiceIndex = new List<int>();

    private void Start()
    {
        canvasObj.SetActive(true);
        RandomizeQuestion();
        EnableInteraction();
    }

    private void EnableInteraction()
    {
        foreach (var col in interactCollider)
        {
            col.enabled = true;
        }
    }

    private void DisableInteraction()
    {
        foreach (var col in interactCollider)
        {
            col.enabled = false;
        }
    }

    private void RandomizeQuestion()
    {
        questionIndex = UnityEngine.Random.Range(0, Qbank.qna.Count);

        while (Qbank.qna[questionIndex].notActive)
        {
            questionIndex = UnityEngine.Random.Range(0, Qbank.qna.Count);
        }

        Qbank.qna[questionIndex].notActive = true;
        currentAnswer = Qbank.qna[questionIndex].answer;

        Debug.Log(currentAnswer);

        RandomizeChoices();
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        Actions.updateText(Qbank.qna[questionIndex].question, UITextType.QuestionText_1);
    }

    private void RandomizeChoices()
    {
        List<int> choiceIndex = new List<int>();
        System.Random rand = new System.Random();
        int number;

        for (int i = 0; i < 4; i++)
        {
            do
            {
                number = rand.Next(0, Qbank.qna[questionIndex].choices.Count);
            } while (choiceIndex.Contains(number));
            choiceIndex.Add(number);
        }

        randomChoiceIndex = choiceIndex.OrderBy(_ => rand.Next()).ToList();

        DisplayChoices();
    }

    private void DisplayChoices()
    {
        Actions.updateChoiceText?.Invoke(Choice.A, Qbank.qna[questionIndex].choices[randomChoiceIndex[0]]);
        Actions.updateChoiceText?.Invoke(Choice.B, Qbank.qna[questionIndex].choices[randomChoiceIndex[1]]);
        Actions.updateChoiceText?.Invoke(Choice.C, Qbank.qna[questionIndex].choices[randomChoiceIndex[2]]);
        Actions.updateChoiceText?.Invoke(Choice.D, Qbank.qna[questionIndex].choices[randomChoiceIndex[3]]);
    }

    private void CheckCorrect(string answer)
    {

        if (currentAnswer == answer)
        {
            Debug.Log("Correct");
            questionCounter++;
            AudioManager.Instance.Play("right_answer");
        }
        else
        {
            Debug.Log("Wrong");
            Actions.punish?.Invoke(answer, quizNumber);
            Qbank.qna[questionIndex].notActive = false;
            AudioManager.Instance.Play("wrong_answer");
        }

        if (stillHasQuestion())
        {
            RandomizeQuestion();
        }
        else
        {
            Destroy(blockedPath);
            questionCounter = 0;
            DisableInteraction();
            canvasObj.SetActive(false);
            this.enabled = false;
        }
    }

    private bool stillHasQuestion()
    {
        return numberOfQuestions > questionCounter;
    }

    private void OnEnable()
    {
        Actions.answer += CheckCorrect;
        Actions.setInventory?.Invoke(false);
    }

    private void OnDisable()
    {
        Actions.answer -= CheckCorrect;
        Actions.setInventory?.Invoke(true);
    }
}
