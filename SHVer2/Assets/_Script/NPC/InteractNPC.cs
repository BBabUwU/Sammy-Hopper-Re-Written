using UnityEngine;
using System.Collections;

public class InteractNPC : MonoBehaviour
{
    private GameObject _playerObj;
    private PlayerInputScript _playerInput;
    private Transform _playerPosition;
    private bool isInteractable = false;
    [SerializeField] private bool isFacingRight = true;
    private bool defaultDirection;
    private NPCDialogue npcDialogue;

    private void Awake()
    {
        //Sets the Initial direction where the NPC is facing.
        LookAtDirection();
        //Last direction of where the NPC is looking at.
        defaultDirection = isFacingRight;

        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerInput = _playerObj.GetComponent<PlayerInputScript>();
        _playerPosition = _playerObj.GetComponent<Transform>();

        npcDialogue = GetComponent<NPCDialogue>();
    }

    private void Update()
    {
        if (isInteractable)
        {
            if (_playerInput.InteractionButtonPressed() && !IsInteractingNPC())
            {
                SwitchDirection();
                LookAtDirection();
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

    private void SwitchDirection()
    {
        if (_playerPosition.position.x > transform.position.x)
        {
            isFacingRight = true;
        }
        else if (_playerPosition.position.x < transform.position.x)
        {
            isFacingRight = false;
        }
    }

    private void LookAtDirection()
    {
        if (isFacingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else if (!isFacingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    public IEnumerator LookBackAtDefaultDirection()
    {
        yield return new WaitForSeconds(1f);
        isFacingRight = defaultDirection;
        LookAtDirection();
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
