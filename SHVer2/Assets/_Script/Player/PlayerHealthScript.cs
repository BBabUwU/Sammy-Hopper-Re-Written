using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{

    private PlayerUIScript _playerUI;
    [SerializeField] internal float maxHealth = 100f;
    [SerializeField] internal float currentHealth = 100f;
    [SerializeField] internal bool isDead = false;

    private void Awake()
    {
        _playerUI = GetComponent<PlayerUIScript>();
    }

    public void PlayerIsDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        _playerUI.SetPlayerHealth(currentHealth);
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;
        _playerUI.SetPlayerHealth(currentHealth);
    }
}
