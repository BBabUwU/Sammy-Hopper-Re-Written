using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxin : MonoBehaviour
{
    public float damageCoolDownTime = 2f;
    private float nextDamageTime = 0;
    public float damage;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!OnCooldown())
            {
                other.GetComponent<PlayerHealth>().DamagePlayer(damage);
                nextDamageTime = Time.time + damageCoolDownTime;
            }
        }
    }
    private bool OnCooldown()
    {
        bool onCooldown = Time.time > nextDamageTime ? false : true;
        return onCooldown;
    }
}
