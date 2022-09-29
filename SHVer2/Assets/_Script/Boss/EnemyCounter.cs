using UnityEngine;
using System;

public class EnemyCounter : MonoBehaviour
{
    public static event Action enemyKilled;

    private void OnDestroy()
    {
        enemyKilled?.Invoke();
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        BossManager.killEnemy += DestroyEnemy;
    }

    private void OnDisable()
    {
        BossManager.killEnemy -= DestroyEnemy;
    }
}
