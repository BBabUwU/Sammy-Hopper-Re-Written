using UnityEngine;

public class ProjectileTarget : MonoBehaviour
{
    [SerializeField] private float fireSpeed;
    [SerializeField] private float damage = 10f;
    private Transform player;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask destroyOnHit;

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
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("BlockedPath") || other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
