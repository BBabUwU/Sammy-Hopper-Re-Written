using UnityEngine;
using System;

public class StartableBossQuiz : Interactable
{
    public QuizDiff difficulty;
    public static event Action<QuizDiff> StartQuiz;
    public override void Interact()
    {
        StartQuiz?.Invoke(difficulty);
    }

    private void DestroyEssence(QuizDiff diff)
    {
        if (diff == difficulty) Destroy(gameObject);
    }

    private void OnEnable()
    {
        BossQuiz.destroyEssence += DestroyEssence;
    }
    private void OnDisable()
    {
        BossQuiz.destroyEssence -= DestroyEssence;
    }
}
