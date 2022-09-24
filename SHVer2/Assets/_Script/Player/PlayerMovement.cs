using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool allowedToMove = true;
    private Rigidbody2D _playerRigidBody;
    private PlayerInput _playerInput;
    private Player _playerScript;
    private PlayerCollision _playerCollision;
    private Animator _playerAnimation;
    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerScript = GetComponent<Player>();
        _playerInput = GetComponent<PlayerInput>();
        _playerCollision = GetComponent<PlayerCollision>();
        _playerAnimation = GetComponent<Animator>();
    }

    internal void HorizontalMovement()
    {
        if (!allowedToMove)
        {
            //stops the player from moving when he is not allowed to move.
            _playerRigidBody.velocity = new Vector2(0, _playerRigidBody.velocity.y);
            return;
        }

        _playerRigidBody.velocity = new Vector2(walkSpeed * _playerInput.HorizontalAxis(), _playerRigidBody.velocity.y);
    }


    internal void Jump()
    {
        if (!allowedToMove) return;
        //Detects jump input
        _playerInput.JumpIsPressed();
        _playerInput.JumpIsReleased();

        //Animation
        if (_playerCollision.IsGrounded()) _playerAnimation.SetBool("Grounded", true);
        else if (!_playerCollision.IsGrounded()) _playerAnimation.SetBool("Grounded", false);

        //Low Jump
        if (_playerInput.JumpIsPressed() && _playerCollision.IsGrounded())
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, jumpPower);
        }
        //High Jump
        if (_playerInput.JumpIsReleased() && _playerRigidBody.velocity.y > 0f)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _playerRigidBody.velocity.y * 0.5f);
        }

        _playerAnimation.SetFloat("Falling", _playerRigidBody.velocity.y);
    }

    internal void FlipPlayer()
    {
        if (!allowedToMove) return;

        if (isFacingRight && _playerInput.HorizontalAxis() < 0f || !isFacingRight && _playerInput.HorizontalAxis() > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    internal void HorizontalMovementAnimation()
    {
        _playerAnimation.SetFloat("RunSpeed", Mathf.Abs(_playerRigidBody.velocity.x));
    }

    //Event functions
    //Listening to game manager.
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Exploration || state == GameState.BossBattle)
        {
            allowedToMove = true;
        }

        else
        {
            allowedToMove = false;
        }
    }
}
