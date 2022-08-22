using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 500f;
    public float currentHealth = 500f;
    public bool isDead;
    private int attackMultiplier = 1;
    private BossUI bossUI;
    private Animator bossAnim;

    private void Awake()
    {
        bossUI = GetComponent<BossUI>();
        bossAnim = GetComponent<Animator>();
        bossUI.SetMaxHealth(maxHealth);
    }

    public void IsDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            bossAnim.SetTrigger("isDead");
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void SetAttackMultiplier(int attackMultiplier)
    {
        this.attackMultiplier = attackMultiplier;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage * attackMultiplier;
        bossUI.SetHealth(currentHealth);
        IsDead();
    }
}
