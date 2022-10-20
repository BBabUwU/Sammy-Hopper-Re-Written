using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public bool isDead = false;
    [HideInInspector] public bool canDamage = true;
    public static event Action<UIHealthType, float> SetMaxHealthUI;
    public static event Action<UIHealthType, float> SetCurrentHealthUI;
    private SpriteRenderer theRenderer;
    private Animator anim;

    private void Awake()
    {
        theRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

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
        if (!isDead && currentHealth <= 0)
        {
            Actions.setAllControls(false);
            isDead = true;
            anim.SetTrigger("Death");
        }
    }

    public void DeadState()
    {
        GameManager.Instance.UpdateGameState(GameState.PlayerDead);
    }


    public void DamagePlayer(float damage)
    {
        if (canDamage)
        {
            StartCoroutine(HurtIndicator());
            currentHealth -= damage;
            if (currentHealth <= 0) currentHealth = 0;
            SetCurrentHealthUI?.Invoke(UIHealthType.PlayerHealthBar, currentHealth);
            canDamage = false;
            StartCoroutine(IFrame());
        }
    }

    private IEnumerator IFrame()
    {
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        SetCurrentHealthUI?.Invoke(UIHealthType.PlayerHealthBar, currentHealth);
    }

    IEnumerator HurtIndicator()
    {
        theRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        theRenderer.color = Color.white;
    }

    private void InstantKill()
    {
        DamagePlayer(maxHealth);
    }

    private void Set_Death(bool x)
    {
        isDead = x;
    }

    private void OnEnable()
    {
        HealthPotion.HealPlayer += HealPlayer;
        UIHealthController.SetSliderValue += SetInitialUIvalues;
        Actions.ResetDeath += Set_Death;
        Actions.SetPlayerHealth += HealPlayer;
    }

    private void OnDisable()
    {
        HealthPotion.HealPlayer -= HealPlayer;
        UIHealthController.SetSliderValue -= SetInitialUIvalues;
        Actions.ResetDeath -= Set_Death;
        Actions.SetPlayerHealth -= HealPlayer;
    }
}
