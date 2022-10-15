using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        _enemyMovement.Patrol();
    }
}
