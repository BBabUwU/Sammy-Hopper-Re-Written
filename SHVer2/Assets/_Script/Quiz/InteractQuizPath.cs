using UnityEngine;
public class InteractQuizPath : Interactable
{
    private QuizScript quiz;
    private void Awake()
    {
        quiz = GetComponent<QuizScript>();
    }

    public override void Interact()
    {
        if (!quiz.quizStarted)
        {
            quiz.enabled = true;
        }
    }


    private void QuizPassed(Quiz quiz)
    {
        if (quiz.isPassed)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnEnable()
    {
        QuizScript.QuizPassed += QuizPassed;
    }

    private void OnDisable()
    {
        QuizScript.QuizPassed -= QuizPassed;
    }
}
