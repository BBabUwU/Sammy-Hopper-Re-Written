using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxin : MonoBehaviour
{
    public float damage;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(12, 23);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph.canDamage)
            {
                ph.DamagePlayer(damage);
                Actions.addEveluation?.Invoke("hit");
            }
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
