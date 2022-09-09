using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OpenVideoPlayer()
    {
        if (playerInput.VideoPlayerButton()) GameManager.Instance.UpdateGameState(GameState.VideoPlayer);
    }
}
