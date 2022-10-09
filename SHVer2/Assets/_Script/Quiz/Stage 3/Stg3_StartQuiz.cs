using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stg3_StartQuiz : Interactable
{
    private Stg3_QuizManager quizManager;
    private BoxCollider2D col;

    private void Awake()
    {
        quizManager = GetComponent<Stg3_QuizManager>();
        col = GetComponent<BoxCollider2D>();
    }

    public override void Interact()
    {
        quizManager.enabled = true;
        col.enabled = false;
    }
}
