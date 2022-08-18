using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void FixedUpdate()
    {
        _enemyMovement.Patrol();
    }

    private void Update()
    {
        _enemyHealth.IsDead();
    }
}
