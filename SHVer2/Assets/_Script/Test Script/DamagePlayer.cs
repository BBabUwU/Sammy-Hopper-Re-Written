using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private PlayerHealthScript _playerHealth;
    private void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealth.DamagePlayer(20f);
        }
    }
}
