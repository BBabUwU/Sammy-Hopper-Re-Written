using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    [SerializeField] private Slider _playerHealthSlider;

    //Health bar
    public void SetPlayerMaxHealth(float playerHealth)
    {
        _playerHealthSlider.maxValue = playerHealth;
        _playerHealthSlider.value = playerHealth;
    }

    public void SetPlayerHealth(float playerHealth)
    {
        _playerHealthSlider.value = playerHealth;
    }

}
