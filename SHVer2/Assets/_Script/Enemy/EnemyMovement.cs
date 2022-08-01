using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] internal bool isFacingRight = true;
    private Rigidbody2D _enemyRigidBody;
    private EnemyCollision _enemyCollision;

    private void Awake()
    {
        _enemyRigidBody = GetComponent<Rigidbody2D>();
        _enemyCollision = GetComponent<EnemyCollision>();
    }

    internal void Patrol()
    {
        if (_enemyCollision.GroundDetected())
        {
            if (isFacingRight)
            {
                MoveRight();
            }

            if (!isFacingRight)
            {
                MoveLeft();
            }
        }

        if (!_enemyCollision.GroundDetected() || _enemyCollision.ObstacleDetected())
        {
            FlipEnemy();
        }
    }

    private void MoveRight()
    {
        _enemyRigidBody.velocity = new Vector2(walkSpeed, _enemyRigidBody.velocity.y);
    }

    private void MoveLeft()
    {
        _enemyRigidBody.velocity = new Vector2(-walkSpeed, _enemyRigidBody.velocity.y);
    }

    private void FlipEnemy()
    {
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
}
