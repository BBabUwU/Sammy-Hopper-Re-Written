using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public bool isDead = false;
    public static event Action<UIHealthType, float> SetMaxHealthUI;
    public static event Action<UIHealthType, float> SetCurrentHealthUI;

    public void SetInitialUIvalues(UIHealthType _sliderType)
    {
        if (_sliderType == UIHealthType.PlayerHealthBar)
        {
            SetMaxHealthUI?.Invoke(UIHealthType.PlayerHealthBar, maxHealth);
            SetCurrentHealthUI?.Invoke(UIHealthType.PlayerHealthBar, currentHealth);
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
        SetCurrentHealthUI?.Invoke(UIHealthType.PlayerHealthBar, currentHealth);
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        SetCurrentHealthUI?.Invoke(UIHealthType.PlayerHealthBar, currentHealth);
    }

    private void InstantKill()
    {
        DamagePlayer(maxHealth);
    }

    private void OnEnable()
    {
        HealthPotion.HealPlayer += HealPlayer;
        UIHealthController.SetSliderValue += SetInitialUIvalues;
    }

    private void OnDisable()
    {
        HealthPotion.HealPlayer -= HealPlayer;
        UIHealthController.SetSliderValue -= SetInitialUIvalues;
    }
}
