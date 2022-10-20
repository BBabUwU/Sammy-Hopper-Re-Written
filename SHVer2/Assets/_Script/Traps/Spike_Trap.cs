using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Trap : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<PlayerHealth>().DamagePlayer(damage);
    }
}
