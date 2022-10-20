using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStraight : MonoBehaviour
{
    private Rigidbody2D _projectileRigidbody;
    [SerializeField] private float damageSpeed;
    [SerializeField] private float damage;
    private void Awake()
    {
        _projectileRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 25);
        ProjectileTravel();
        StartCoroutine(SelfDestruct());
    }

    private void ProjectileTravel()
    {
        _projectileRigidbody.velocity = transform.right * damageSpeed;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("BlockedPath"))
        {
            Destroy(gameObject);
        }
    }
}
