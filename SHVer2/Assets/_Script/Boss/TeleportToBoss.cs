using UnityEngine;

public class TeleportToBoss : MonoBehaviour
{

    [SerializeField] private Transform pointB;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateGameState(GameState.BossBattle);
            other.transform.position = pointB.position;
        }
    }
}
