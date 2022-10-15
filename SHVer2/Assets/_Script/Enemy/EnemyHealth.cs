using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] internal float maxHealth = 100f;
    [SerializeField] internal float currentHealth = 100f;
    [SerializeField] internal bool isDead = false;
    private EnemyCollision _enemyCollision;
    private SpriteRenderer theRenderer;

    private void Awake()
    {
        _enemyCollision = GetComponent<EnemyCollision>();
        theRenderer = GetComponent<SpriteRenderer>();
    }

    public void Damage(int damageAmount)
    {
        StartCoroutine(HurtIndicator());
        currentHealth -= damageAmount;
        IsDead();
    }

    IEnumerator HurtIndicator()
    {
        theRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        theRenderer.color = Color.white;
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
