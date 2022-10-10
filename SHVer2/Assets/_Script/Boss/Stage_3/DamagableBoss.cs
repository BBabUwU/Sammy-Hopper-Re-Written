using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableBoss : MonoBehaviour, IDamageable
{
    public void Damage(int damage)
    {
        Actions.damageBoss?.Invoke(damage);
    }
}
