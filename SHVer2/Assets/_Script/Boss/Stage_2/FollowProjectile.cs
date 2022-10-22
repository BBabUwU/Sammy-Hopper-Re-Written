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
    public bool isFacingRight = true;
    private SpriteRenderer theRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        theRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, fireSpeed * Time.deltaTime);
        FlipEnemy();
    }

    public void Damage(int damage)
    {
        StartCoroutine(HurtIndicator());
        projHealth -= damage;

        if (projHealth <= 0)
        {
            DestroyProjectile();
        }
    }

    private void FlipEnemy()
    {
        if (player.position.x > transform.position.x)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }

        if (isFacingRight)
        {
            isFacingRight = false;
            transform.eulerAngles = new Vector3(0, -180, 0);
        }

        else if (!isFacingRight)
        {
            isFacingRight = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
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

    IEnumerator HurtIndicator()
    {
        theRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        theRenderer.color = Color.white;
    }

    private void DestroyProjectile()
    {
        Actions.decreaseProjectileCounter?.Invoke();
        Destroy(gameObject);
    }
}
