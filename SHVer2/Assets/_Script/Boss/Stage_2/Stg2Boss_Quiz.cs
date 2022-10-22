using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Stg2Boss_Quiz : MonoBehaviour
{
    [SerializeField] private Stg2_QuestionBank Qbank;
    [SerializeField] private List<BoxCollider2D> interactCollider;
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private int damageBoss;
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

    private void StartQuiz()
    {
        EnableInteraction();
        canvasObj.SetActive(true);
    }

    private void StopQuiz()
    {
        DisableInteraction();
        canvasObj.SetActive(false);
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
        SetInactive();

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
            AudioManager.Instance.Play("right_answer");
            Debug.Log("Right");
            RandomizeQuestion();
            Actions.spawnEnemy?.Invoke();
            Actions.damageBoss?.Invoke(damageBoss);
        }
        else
        {
            AudioManager.Instance.Play("wrong_answer");
            Debug.Log("Wrong");
            Actions.addEveluation?.Invoke("mistake");
            Actions.spawnEnemy?.Invoke();
            Actions.punish?.Invoke(answer, quizNumber);
            Qbank.qna[questionIndex].notActive = false;
            RandomizeQuestion();
        }
    }

    private void SetInactive()
    {
        if (AllActive())
        {
            foreach (var qbank in Qbank.qna)
            {
                qbank.notActive = false;
            }
        }
    }

    private bool AllActive()
    {
        bool allActive = true;

        for (int i = 0; i < Qbank.qna.Count; i++)
        {
            if (Qbank.qna[i].notActive == false)
            {
                allActive = false;
            }
        }

        return allActive;
    }

    private void OnEnable()
    {
        Actions.answer += CheckCorrect;
        Actions.startQuiz += StartQuiz;
        Actions.stopQuiz += StopQuiz;
        Actions.startTime += StartQuiz;
    }

    private void OnDisable()
    {
        Actions.answer -= CheckCorrect;
        Actions.startQuiz -= StartQuiz;
        Actions.stopQuiz -= StopQuiz;
        Actions.startTime -= StartQuiz;
    }
}
