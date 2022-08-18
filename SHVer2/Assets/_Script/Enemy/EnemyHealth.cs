using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] internal float maxHealth = 100f;
    [SerializeField] internal float currentHealth = 100f;
    [SerializeField] internal bool isDead = false;
    private EnemyCollision _enemyCollision;

    private void Awake()
    {
        _enemyCollision = GetComponent<EnemyCollision>();
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    public void IsDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
}
