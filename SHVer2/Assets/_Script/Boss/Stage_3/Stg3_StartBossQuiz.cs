using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stg3_StartBossQuiz : Interactable
{
    [SerializeField] private Stg3_BossManager bossManager;
    private Stg3_Quiz quiz;

    private void Awake()
    {
        quiz = GetComponent<Stg3_Quiz>();
    }
    public override void Interact()
    {
        if (bossManager.CanAnswer())
        {
            GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
            bossManager.shootManager.canShoot = false;
            quiz.enabled = true;
        }
    }
}
