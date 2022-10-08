using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : MonoBehaviour, IDamageable
{
    [SerializeField] private float projHealth = 40;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float damage = 20f;
    private Transform player;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, fireSpeed * Time.deltaTime);
        transform.up = player.position - transform.position;
    }

    public void Damage(float damage)
    {
        projHealth -= damage;

        if (projHealth <= 0)
        {
            DestroyProjectile();
        }
    }

    //moveDirection = (player.position - transform.position).normalized * fireSpeed;
    //rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    //Destroy(gameObject, 5f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Actions.decreaseProjectileCounter?.Invoke();
        Destroy(gameObject);
    }
}
