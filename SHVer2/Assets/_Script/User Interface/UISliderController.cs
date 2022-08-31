using UnityEngine;
using UnityEngine.UI;

public enum UISliderType
{
    PlayerHealthBar,
    BossHealthBar
}

public class UISliderController : MonoBehaviour
{
    [SerializeField] private UISliderType sliderType;
    private Slider sliderUI;

    private void Awake()
    {
        sliderUI = GetComponent<Slider>();
    }

    public void SetMaxHealth(UISliderType _sliderType, float playerHealth)
    {
        if (sliderType == _sliderType)
        {
            sliderUI.maxValue = playerHealth;
            sliderUI.value = playerHealth;
        }
    }

    public void SetHealth(UISliderType _sliderType, float playerHealth)
    {
        if (sliderType == _sliderType)
        {
            sliderUI.value = playerHealth;
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
