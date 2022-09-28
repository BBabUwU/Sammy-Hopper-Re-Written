using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuestionBank : MonoBehaviour
{
    [NonReorderable]
    [SerializeField]
    private List<Stage1QnATemplate> questionAndAnswers;

    private List<Stage1QnATemplate> GetQnA()
    {
        return questionAndAnswers;
    }

    private void OnEnable()
    {
        ExplorationQuiz.QuestionBank += GetQnA;
    }

    private void OnDisable()
    {
        ExplorationQuiz.QuestionBank -= GetQnA;
    }

}
