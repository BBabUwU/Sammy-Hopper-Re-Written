using UnityEngine;
using System;

public class BossHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 500f;
    public float currentHealth = 500f;
    public bool isDead;
    private int attackMultiplier = 1;
    private Animator bossAnim;
    public static event Action<UISliderType, float> SetMaxHealthUI;
    public static event Action<UISliderType, float> SetCurrentHealthUI;


    private void Awake()
    {
        bossAnim = GetComponent<Animator>();
        SetMaxHealthUI?.Invoke(UISliderType.BossHealthBar, maxHealth);
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

    public void SetPlayerAttackMultiplier(int attackMultiplier)
    {
        this.attackMultiplier = attackMultiplier;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage * attackMultiplier;
        SetCurrentHealthUI?.Invoke(UISliderType.BossHealthBar, currentHealth);
        IsDead();
    }
}
