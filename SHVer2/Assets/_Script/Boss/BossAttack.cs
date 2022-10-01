using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttack : MonoBehaviour
{

    public bool BossTurn;
    public GameObject projectTilePrefab;
    public Transform projectilePoint;

    public float atkCoolDownTime = 2f;
    private float nextFireTime = 0;
    private int EnemiesKilled;

    public static event Action TurnOver;

    private void Awake()
    {
        BossTurn = false;
    }

    private void Update()
    {
        if (BossTurn && !AtkOnCooldown())
        {
            AttackPlayer();
            nextFireTime = Time.time + atkCoolDownTime;
        }
    }

    private void AttackPlayer()
    {
        Instantiate(projectTilePrefab, projectilePoint.position, Quaternion.identity);
    }

    private void SetTurn(bool turn)
    {
        BossTurn = turn;
    }

    private bool AtkOnCooldown()
    {
        bool atkOnCooldown = Time.time > nextFireTime ? false : true;
        return atkOnCooldown;
    }

    private void BossTurnOver()
    {
        if (BossTurn)
        {
            if (EnemiesKilled == 6)
            {
                TurnOver?.Invoke();
                BossTurn = false;
                EnemiesKilled = 0;
            }
            else
            {
                EnemiesKilled++;
                Debug.Log(EnemiesKilled);
            }
        }
    }

    private void OnEnable()
    {
        BossManager.bossTurn += SetTurn;

        EnemyCounter.enemyKilled += BossTurnOver;
    }

    private void OnDisable()
    {
        BossManager.bossTurn -= SetTurn;

        EnemyCounter.enemyKilled -= BossTurnOver;
    }
}
