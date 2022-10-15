using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LineOfSight : MonoBehaviour
{
    public float lineOfSite;

    public bool InRange(Transform player)
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        return distanceFromPlayer < lineOfSite;
    }

    private void OnDrawGizmos()
    {
        //Sight
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
