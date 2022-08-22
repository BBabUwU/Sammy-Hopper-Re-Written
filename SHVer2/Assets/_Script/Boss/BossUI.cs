using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private Slider bossHealthSlider;

    //Health bar
    public void SetMaxHealth(float playerHealth)
    {
        bossHealthSlider.maxValue = playerHealth;
        bossHealthSlider.value = playerHealth;
    }

    public void SetHealth(float playerHealth)
    {
        bossHealthSlider.value = playerHealth;
    }
}
