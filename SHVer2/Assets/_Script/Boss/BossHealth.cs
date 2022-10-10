using UnityEngine;
using System;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 400f;
    public float currentHealth = 400f;
    public float damageLimit;
    private float damageInflicted;
    public bool isDead;
    private Animator bossAnim;
    public static event Action<UIHealthType, float> SetMaxHealthUI;
    public static event Action<UIHealthType, float> SetCurrentHealthUI;
    public static event Action BossDefeated;


    private void Awake()
    {
        bossAnim = GetComponent<Animator>();
    }

    public void SetInitialUIvalues(UIHealthType _sliderType)
    {
        if (_sliderType == UIHealthType.BossHealthBar)
        {
            SetMaxHealthUI?.Invoke(UIHealthType.BossHealthBar, maxHealth);
            SetCurrentHealthUI?.Invoke(UIHealthType.BossHealthBar, currentHealth);
        }
    }

    public void IsDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            BossDefeated?.Invoke();
            Actions.bossDefeated?.Invoke();
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        damageInflicted += damage;

        if (damageLimit < damageInflicted)
        {
            Actions.damageLimitReached?.Invoke();
            damageInflicted = 0;
        }

        currentHealth -= damage;
        SetCurrentHealthUI?.Invoke(UIHealthType.BossHealthBar, currentHealth);
        IsDead();
    }

    private void OnEnable()
    {
        UIHealthController.SetSliderValue += SetInitialUIvalues;

        BossManager.damageBoss += Damage;

        Actions.damageBoss += Damage;
    }

    private void OnDisable()
    {
        UIHealthController.SetSliderValue -= SetInitialUIvalues;

        BossManager.damageBoss -= Damage;

        Actions.damageBoss -= Damage;
    }
}
