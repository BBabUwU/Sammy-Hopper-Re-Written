using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private Transform obstacleDetection;
    [SerializeField] private Transform groundDetection;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask blockedPathLayer;
    [SerializeField] private float groundRayDistance = 1f;
    [SerializeField] private float boxWidth = 1f;
    [SerializeField] private float boxHeight = 1f;
    [SerializeField] private bool showDetectionGizmo;

    //Enemy attack script
    private EnemyAttack _enemyAttack;
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyHealth = GetComponent<EnemyHealth>();
        IgnoreCollisions();
    }

    private void IgnoreCollisions()
    {
        //15 = enemy 14 = Essence
        Physics2D.IgnoreLayerCollision(15, 14);
        Physics2D.IgnoreLayerCollision(15, 15);
    }

    private Vector2 ObstacleCollisionSize()
    {
        return new Vector2(boxWidth, boxHeight);
    }

    internal bool ObstacleDetected()
    {
        RaycastHit2D collisionHasDetected = Physics2D.BoxCast(obstacleDetection.position, ObstacleCollisionSize(), 0, Vector2.right, 0, wallLayer | groundLayer | blockedPathLayer);
        return collisionHasDetected.collider != null;
    }

    internal bool GroundDetected()
    {
        RaycastHit2D groundHasDetected = Physics2D.Raycast(groundDetection.position, Vector2.down, groundRayDistance, groundLayer);
        return groundHasDetected.collider != null;
    }

    private void OnDrawGizmos()
    {
        if (showDetectionGizmo)
        {
            Gizmos.color = Color.blue;
            //Display Obstacle
            Gizmos.DrawWireCube(obstacleDetection.position, ObstacleCollisionSize());
            //Display Ground ray
            Gizmos.DrawRay(groundDetection.position, Vector2.down * groundRayDistance);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Gets component of the collided object. E.g. Player projectile object will get component PlayerWeaponProjectile. 
        if (other.CompareTag("Player"))
        {
            PlayerHealth _playerHealth = other.GetComponent<PlayerHealth>();
            _playerHealth.DamagePlayer(_enemyAttack.GetEnemyDamage());
        }
    }
}
