using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] internal float maxHealth = 100f;
    [SerializeField] internal float currentHealth = 100f;
    [SerializeField] internal bool isDead = false;
    private EnemyCollision _enemyCollision;

    private void Awake()
    {
        _enemyCollision = GetComponent<EnemyCollision>();
    }

    internal void DamageEnemy(float damage)
    {
        currentHealth -= damage;
    }

    internal void EnemyIsDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
}
