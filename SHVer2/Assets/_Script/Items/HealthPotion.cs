using UnityEngine;
using System;
public class HealthPotion : MonoBehaviour, IPickable
{
    [SerializeField] private float health;
    public static Action<float> HealPlayer;
    public void PickUp()
    {
        HealPlayer?.Invoke(health);
        Destroy(gameObject);
    }
}
