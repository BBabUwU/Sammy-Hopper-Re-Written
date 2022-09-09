using UnityEngine;
using UnityEngine.UI;
using System;

public enum UIHealthType
{
    PlayerHealthBar,
    BossHealthBar
}

public class UIHealthController : MonoBehaviour
{
    [SerializeField] private UIHealthType sliderType;
    private Slider sliderUI;
    public static event Action<UIHealthType> SetSliderValue;
    private bool hasNotSetvalue;

    private void Awake()
    {
        sliderUI = GetComponent<Slider>();
        hasNotSetvalue = true;
    }

    private void Start()
    {
        if (hasNotSetvalue)
        {
            SetSliderValue?.Invoke(sliderType);
            hasNotSetvalue = false;
        }
    }

    public void SetMaxHealth(UIHealthType _sliderType, float health)
    {
        Debug.Log("Set max health: " + _sliderType);
        if (sliderType == _sliderType)
        {
            sliderUI.maxValue = health;
        }
    }

    public void SetHealth(UIHealthType _sliderType, float health)
    {
        if (sliderType == _sliderType)
        {
            sliderUI.value = health;
        }
    }

    private void OnEnable()
    {
        //Player
        PlayerHealth.SetMaxHealthUI += SetMaxHealth;
        PlayerHealth.SetCurrentHealthUI += SetHealth;

        //boss
        BossHealth.SetMaxHealthUI += SetMaxHealth;
        BossHealth.SetCurrentHealthUI += SetHealth;
    }

    private void OnDisable()
    {
        PlayerHealth.SetMaxHealthUI -= SetMaxHealth;
        PlayerHealth.SetCurrentHealthUI -= SetHealth;

        BossHealth.SetMaxHealthUI -= SetMaxHealth;
        BossHealth.SetCurrentHealthUI -= SetHealth;
    }
}
