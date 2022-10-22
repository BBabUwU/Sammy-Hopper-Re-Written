using UnityEngine;
using System;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 400f;
    public float currentHealth = 400f;
    public bool enableDamageLimit = false;
    public float damageLimit;
    private float damageInflicted;
    public bool isDead;
    private Animator bossAnim;
    public static event Action<UIHealthType, float> SetMaxHealthUI;
    public static event Action<UIHealthType, float> SetCurrentHealthUI;
    public static event Action BossDefeated;
    private SpriteRenderer theRenderer;


    private void Awake()
    {
        bossAnim = GetComponent<Animator>();
        theRenderer = GetComponent<SpriteRenderer>();
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

        StartCoroutine(HurtIndicator());
        if (bossAnim != null) bossAnim.SetTrigger("Hurt");

        if (damageLimit < damageInflicted && enableDamageLimit)
        {
            Actions.damageLimitReached?.Invoke();
            damageInflicted = 0;
        }

        currentHealth -= damage;
        SetCurrentHealthUI?.Invoke(UIHealthType.BossHealthBar, currentHealth);
        IsDead();
    }

    IEnumerator HurtIndicator()
    {
        theRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        theRenderer.color = Color.white;
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
