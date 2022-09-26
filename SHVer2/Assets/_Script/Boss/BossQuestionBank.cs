using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuestionBank : MonoBehaviour
{
    //Essence (Questions)
    //Where the questions will be obtained

    //Essence
    [NonReorderable]
    [SerializeField]
    private List<Chapter1QnATemplate> easyQuestions = new List<Chapter1QnATemplate>();


    private List<Chapter1QnATemplate> GetEasyQuestions()
    {
        return easyQuestions;
    }

    private void OnEnable()
    {
        BossManager.questionBank += GetEasyQuestions;
    }

    private void OnDisable()
    {
        BossManager.questionBank -= GetEasyQuestions;
    }
}
