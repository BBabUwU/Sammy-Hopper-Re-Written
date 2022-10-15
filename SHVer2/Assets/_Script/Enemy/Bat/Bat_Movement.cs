using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Movement : MonoBehaviour
{
    public float speed;
    private Bat_Attack batAtk;
    private Transform player;
    private Rigidbody2D bat;
    private Enemy_LineOfSight detection;
    public bool isFacingRight = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bat = GetComponent<Rigidbody2D>();

        detection = GetComponent<Enemy_LineOfSight>();

        batAtk = GetComponent<Bat_Attack>();
    }

    public void MoveTowardsPlayer()
    {
        FlipEnemy();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (detection.InRange(player) && distanceFromPlayer > batAtk.attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
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
}
