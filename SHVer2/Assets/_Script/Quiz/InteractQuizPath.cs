using UnityEngine;

[RequireComponent(typeof(QuizScript))]
public class InteractQuizPath : Interactable
{
    private QuizScript quizScript;
    private void Awake()
    {
        quizScript = GetComponent<QuizScript>();
    }

    public override void Interact()
    {
        if (!quizScript.quizStarted)
        {
            quizScript.enabled = true;
        }
    }

    private void QuizPassed(Quiz quiz)
    {
        if (quizScript.quizNumber == quiz.quizNumber)
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
