using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuestionBank : MonoBehaviour
{
    [NonReorderable]
    [SerializeField]
    private List<Chapter1QnATemplate> questionAndAnswers;

    private List<Chapter1QnATemplate> GetQnA()
    {
        return questionAndAnswers;
    }

    private void OnEnable()
    {
        Quiz.QuestionBank += GetQnA;
    }

    private void OnDisable()
    {
        Quiz.QuestionBank -= GetQnA;
    }

}
