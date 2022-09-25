using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossQuiz))]
public class StartableBossQuiz : Interactable
{
    private BossQuiz quiz;

    private void Awake()
    {
        quiz = GetComponent<BossQuiz>();
    }

    public override void Interact()
    {
        GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
        quiz.enabled = true;
    }
}
