using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExplorationQuiz))]
public class StartableQuiz : Interactable
{
    private ExplorationQuiz quiz;

    private void Awake()
    {
        quiz = GetComponent<ExplorationQuiz>();
    }

    public override void Interact()
    {
        GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        quiz.enabled = true;
    }
}
