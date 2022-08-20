using UnityEngine;
using System;

public class TeleportToBoss : MonoBehaviour
{

    [SerializeField] private Transform pointB;
    public static event Action Teleported;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateGameState(GameState.AnsweringQuiz);
            other.transform.position = pointB.position;
            Teleported?.Invoke();
        }
    }
}
