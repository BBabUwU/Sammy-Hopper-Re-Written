using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : MonoBehaviour
{
    private Bat_Movement enemyMovement;
    private Bat_Attack batAttack;

    private void Awake()
    {
        enemyMovement = GetComponent<Bat_Movement>();
        batAttack = GetComponent<Bat_Attack>();
    }

    private void Update()
    {
        enemyMovement.MoveTowardsPlayer();
        batAttack.AttackPlayer();
    }
}
