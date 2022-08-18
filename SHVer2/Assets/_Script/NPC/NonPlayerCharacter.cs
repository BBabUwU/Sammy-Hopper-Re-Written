using UnityEngine;
using System.Collections;

public class NonPlayerCharacter : MonoBehaviour
{
    //Components
    private GameObject _playerObj;
    private PlayerInput _playerInput;
    private Transform _playerPosition;
    private bool defaultDirection;
    private InteractionDialogue npcDialogue;
    private bool isInteractable = false;
    [SerializeField] private bool isFacingRight = true;

    private void Awake()
    {
        //Sets the Initial direction where the NPC is facing.
        LookAtDirection();
        //Last direction of where the NPC is looking at.
        defaultDirection = isFacingRight;

        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerInput = _playerObj.GetComponent<PlayerInput>();
        _playerPosition = _playerObj.GetComponent<Transform>();

        npcDialogue = GetComponent<InteractionDialogue>();
    }

    private bool IsInteractingNPC()
    {
        return GameManager.Instance.gameState == GameState.NPCInteraction;
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
}
