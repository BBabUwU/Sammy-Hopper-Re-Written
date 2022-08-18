using UnityEngine;
using System;

public class KillQuest : MonoBehaviour
{
    [SerializeField] private QuestNumber questEnemy;
    public static Action<QuestNumber> enemyKilled;

    private void OnDisable()
    {
        enemyKilled?.Invoke(questEnemy);
    }
}
