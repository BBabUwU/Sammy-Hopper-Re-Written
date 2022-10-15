using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Attack : MonoBehaviour
{
    public float atkCoolDownTime = 2f;
    public bool allowedToAttack = true;
    private float nextAtkTime = 0;
    public float attackRange;
    public LayerMask playerLayer;
    private Collider2D playerCol;

    public void AttackPlayer()
    {
        if (!AtkOnCooldown() && AttackRange())
        {
            playerCol.GetComponent<PlayerHealth>().DamagePlayer(10f);
            nextAtkTime = Time.time + atkCoolDownTime;
        }

    }

    private bool AtkOnCooldown()
    {
        bool atkOnCooldown = Time.time > nextAtkTime ? false : true;
        return atkOnCooldown;
    }

    private bool AttackRange()
    {
        playerCol = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        return playerCol != null;
    }

    private void OnDrawGizmos()
    {
        //Sight
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
