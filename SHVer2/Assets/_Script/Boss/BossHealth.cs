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
    }

    public void SetInitialUIvalues(UISliderType _sliderType)
    {
        if (_sliderType == UISliderType.BossHealthBar)
        {
            SetMaxHealthUI?.Invoke(UISliderType.BossHealthBar, maxHealth);
            SetCurrentHealthUI?.Invoke(UISliderType.BossHealthBar, currentHealth);
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

    public void SetPlayerAttackMultiplier(int _attackMultiplier)
    {
        attackMultiplier = _attackMultiplier;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage * attackMultiplier;
        SetCurrentHealthUI?.Invoke(UISliderType.BossHealthBar, currentHealth);
        IsDead();
    }

    private void OnEnable()
    {
        BossManager.SetPAttackMultiplier += SetPlayerAttackMultiplier;


        UISliderController.SetSliderValue += SetInitialUIvalues;
    }

    private void OnDisable()
    {
        BossManager.SetPAttackMultiplier -= SetPlayerAttackMultiplier;


        UISliderController.SetSliderValue -= SetInitialUIvalues;
    }
}
