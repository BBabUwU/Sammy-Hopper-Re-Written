using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorProjectile : MonoBehaviour
{
    public bool isParriable;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float damage = 20f;
    private Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        MeteorTravel();
    }

    private void MeteorTravel()
    {
        rb.velocity = -transform.up * fireSpeed;
        Destroy(gameObject, 20f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(damage);
            DestroyProjectile();
        }

        if (other.CompareTag("ParryBlock") && isParriable)
        {
            Actions.parried?.Invoke();
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
