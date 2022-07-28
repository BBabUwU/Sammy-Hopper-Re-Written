using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    private PlayerScript _playerScript;
    [SerializeField] internal float health = 100f;
    [SerializeField] internal bool isDead = false;
    private void Awake()
    {
        _playerScript = GetComponent<PlayerScript>();
    }
    internal void Playerhealth()
    {
        HealPlayer();
        DamagePlayer();
        PlayerIsDead();
    }

    private void PlayerIsDead()
    {
        if (health <= 0)
        {
            isDead = true;
        }
    }

    private void DamagePlayer()
    {
        //Postpone
    }

    private void HealPlayer()
    {
        //Postpone
    }
}
