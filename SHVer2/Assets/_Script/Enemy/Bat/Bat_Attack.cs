using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Attack : MonoBehaviour
{
    public float attackRange;
    public LayerMask playerLayer;
    private Collider2D playerCol;

    public void AttackPlayer()
    {
        if (AttackRange())
        {
            playerCol.GetComponent<PlayerHealth>().DamagePlayer(10f);
        }
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
