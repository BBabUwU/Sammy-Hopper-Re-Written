using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public bool isDead = false;
    public static event Action<UISliderType, float> SetMaxHealthUI;
    public static event Action<UISliderType, float> SetCurrentHealthUI;

    public void SetInitialUIvalues(UISliderType _sliderType)
    {
        if (_sliderType == UISliderType.PlayerHealthBar)
        {
            SetMaxHealthUI?.Invoke(UISliderType.PlayerHealthBar, maxHealth);
            SetCurrentHealthUI?.Invoke(UISliderType.PlayerHealthBar, currentHealth);
        }
    }

    public void IsDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            GameManager.Instance.UpdateGameState(GameState.PlayerDead);
        }
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) currentHealth = 0;
        SetCurrentHealthUI?.Invoke(UISliderType.PlayerHealthBar, currentHealth);
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        SetCurrentHealthUI?.Invoke(UISliderType.PlayerHealthBar, currentHealth);
    }

    private void InstantKill()
    {
        DamagePlayer(maxHealth);
    }

    private void OnEnable()
    {
        HealthPotion.HealPlayer += HealPlayer;

        BossManager.KillPlayer += InstantKill;

        UISliderController.SetSliderValue += SetInitialUIvalues;
    }

    private void OnDisable()
    {
        HealthPotion.HealPlayer -= HealPlayer;

        BossManager.KillPlayer -= InstantKill;

        UISliderController.SetSliderValue -= SetInitialUIvalues;
    }
}
