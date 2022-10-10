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
        Actions.updateText(Qbank.qna[questionIndex].question, UITextType.QuestionText);
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
            Actions.incrementQuiz?.Invoke(quizNumber);
            DisableInteraction();
            canvasObj.SetActive(false);
            this.enabled = false;
        }
        else
        {
            Debug.Log("Wrong");
            Actions.punish?.Invoke(answer);
            Qbank.qna[questionIndex].notActive = false;
            RandomizeQuestion();
        }
    }

    private void OnEnable()
    {
        Actions.answer += CheckCorrect;
    }

    private void OnDisable()
    {
        Actions.answer -= CheckCorrect;
    }
}