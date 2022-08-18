using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 10f;
    Transform player;
    Rigidbody2D bossRigidBody;
    Boss boss;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossRigidBody = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, bossRigidBody.position.y);
        Vector2 newPos = Vector2.MoveTowards(bossRigidBody.position, target, speed * Time.fixedDeltaTime);

        bossRigidBody.MovePosition(newPos);

        Debug.Log(Vector2.Distance(player.position, bossRigidBody.position));

        //Distance from the player
        if (Vector2.Distance(player.position, bossRigidBody.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
