using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Trap : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private Transform firePoint;
    private bool canShoot = true;
    public float coolDown = 3f;
    [SerializeField] private bool hasRange = false;
    [SerializeField] private bool enableGizmo = false;
    public float lineOfSite;
    private Transform player;
    public LayerMask playerLayer;

    private void Awake()
    {
        if (hasRange) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (hasRange)
        {
            if (canShoot && InRange())
            {
                Debug.Log("Here");
                StartCoroutine(AttackPlayer());
                canShoot = false;
            }
        }
        else
        {
            if (canShoot)
            {
                StartCoroutine(AttackPlayer());
                canShoot = false;
            }
        }
    }

    private bool InRange()
    {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, lineOfSite, playerLayer);
        return playerCol != null;
    }

    private void OnDrawGizmos()
    {
        if (!enableGizmo) return;
        //Sight
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(coolDown);
        Instantiate(fire, firePoint.position, firePoint.rotation);
        canShoot = true;
    }
}
