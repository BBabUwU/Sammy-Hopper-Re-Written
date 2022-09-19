using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Quiz))]
public class StartableQuiz : Interactable
{
    private Quiz quiz;

    private void Awake()
    {
        quiz = GetComponent<Quiz>();
    }

    public override void Interact()
    {
        GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        quiz.enabled = true;
    }
}
