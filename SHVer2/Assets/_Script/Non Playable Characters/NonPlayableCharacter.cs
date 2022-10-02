using UnityEngine;
using System.Collections;

public class NonPlayableCharacter : MonoBehaviour
{
    //Components
    private SpriteRenderer spriteRenderer;
    private GameObject _playerObj;
    private PlayerInput _playerInput;
    private Transform _playerPosition;
    private bool defaultDirection;
    [SerializeField] private bool isFacingRight = true;

    private void Awake()
    {
        //Last direction of where the NPC is looking at.
        defaultDirection = isFacingRight;
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerPosition = _playerObj.GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SwitchDirection()
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

    public void LookAtDirection()
    {
        if (isFacingRight)
        {
            //transform.eulerAngles = new Vector3(0, 0, 0);
            spriteRenderer.flipX = false;
        }

        else if (!isFacingRight)
        {
            //transform.eulerAngles = new Vector3(0, -180, 0);
            spriteRenderer.flipX = true;
        }
    }

    public IEnumerator LookBackAtDefaultDirection()
    {
        yield return new WaitForSeconds(1f);
        isFacingRight = defaultDirection;
        LookAtDirection();
    }
}
