using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParriableProj : MonoBehaviour
{

    [SerializeField] private float fireSpeed;
    [SerializeField] private float damage = 20f;
    private Transform player;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [SerializeField] private bool parriable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        moveDirection = (player.position - transform.position).normalized * fireSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        transform.up = player.position - transform.position;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(damage);
            DestroyProjectile();
        }

        if (other.CompareTag("ParryBlock"))
        {
            if (parriable)
            {
                Actions.parried?.Invoke();
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
