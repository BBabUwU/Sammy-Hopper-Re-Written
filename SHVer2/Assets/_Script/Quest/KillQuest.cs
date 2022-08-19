using UnityEngine;
using System;

public class KillQuest : MonoBehaviour
{
    [SerializeField] private int questEnemy;
    public static Action<int> enemyKilled;

    private void OnDisable()
    {
        enemyKilled?.Invoke(questEnemy);
    }
}
