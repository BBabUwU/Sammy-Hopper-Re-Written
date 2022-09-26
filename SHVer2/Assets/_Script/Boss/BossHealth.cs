using UnityEngine;
using System;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 500f;
    public float currentHealth = 500f;
    public bool isDead;
    private int attackMultiplier = 40;
    private Animator bossAnim;
    public static event Action<UIHealthType, float> SetMaxHealthUI;
    public static event Action<UIHealthType, float> SetCurrentHealthUI;


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
            bossAnim.SetTrigger("isDead");
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        currentHealth -= damage * attackMultiplier;
        SetCurrentHealthUI?.Invoke(UIHealthType.BossHealthBar, currentHealth);
        IsDead();
    }

    private void OnEnable()
    {
        UIHealthController.SetSliderValue += SetInitialUIvalues;

        BossManager.damageBoss += Damage;

    }

    private void OnDisable()
    {
        UIHealthController.SetSliderValue -= SetInitialUIvalues;

        BossManager.damageBoss -= Damage;
    }
}
