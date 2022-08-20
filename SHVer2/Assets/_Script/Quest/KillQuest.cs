using UnityEngine;
using System;

public class KillQuest : MonoBehaviour
{
    [SerializeField] private int questEnemyID;
    public static Action<int> enemyKilled;

    private void OnDisable()
    {
        enemyKilled?.Invoke(questEnemyID);
    }
}
