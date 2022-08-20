using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerUI _playerUI;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public bool isDead = false;

    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
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
        _playerUI.SetPlayerHealth(currentHealth);
    }

    public void HealPlayer(float heal)
    {
        currentHealth += heal;
        _playerUI.SetPlayerHealth(currentHealth);
    }
}
