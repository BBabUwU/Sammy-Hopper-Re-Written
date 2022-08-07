using UnityEngine;

public class InteractNPC : MonoBehaviour
{
    private PlayerInputScript _playerInput;
    private bool isInteractable = false;
    private NPCDialogue npcDialogue;

    private void Awake()
    {
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputScript>();

        npcDialogue = GetComponent<NPCDialogue>();
    }

    private void Update()
    {
        if (isInteractable)
        {
            if (_playerInput.InteractionButtonPressed() && !IsInteractingNPC())
            {
                GameManager.Instance.UpdateGameState(GameState.InteractingNPC);
                npcDialogue.StartDialogue();
            }

            else if (_playerInput.InteractionButtonPressed() && IsInteractingNPC())
            {
                npcDialogue.SkipText();
            }
        }
    }

    private bool IsInteractingNPC()
    {
        return GameManager.Instance.gameState == GameState.InteractingNPC;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isInteractable = false;
    }
}
