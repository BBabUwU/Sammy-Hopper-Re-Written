using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage = 10f;

    internal float GetEnemyDamage()
    {
        return attackDamage;
    }

}
