using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    private Animator bossAnimator;
    public Transform player;
    public bool isFlipped = false;

    private void Awake()
    {
        bossAnimator = GetComponent<Animator>();
    }

    private void StartBossFight()
    {
        bossAnimator.SetBool("BossStarted", true);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= 1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    /*
    private void OnDestroy()
    {
        GameManager.Instance.UpdateGameState(GameState.LevelComplete);
    }
    */
}
