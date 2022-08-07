using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool allowedToMove = true;
    private Rigidbody2D _playerRigidBody;
    private PlayerInputScript _playerInput;
    private PlayerScript _playerScript;
    private PlayerCollisionScript _playerCollision;
    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerScript = GetComponent<PlayerScript>();
        _playerInput = GetComponent<PlayerInputScript>();
        _playerCollision = GetComponent<PlayerCollisionScript>();
    }

    internal void PLayerHorizontalMovement()
    {
        if (!allowedToMove)
        {
            //stops the player from moving when he is not allowed to move.
            _playerRigidBody.velocity = new Vector2(0, _playerRigidBody.velocity.y);
            return;
        }

        _playerRigidBody.velocity = new Vector2(walkSpeed * _playerInput.HorizontalAxis(), _playerRigidBody.velocity.y);
    }


    internal void PlayerJump()
    {
        if (!allowedToMove) return;
        //Detects jump input
        _playerInput.JumpIsPressed();
        _playerInput.JumpIsReleased();
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

        if (state == GameState.AnsweringQuiz || state == GameState.InteractingNPC || state == GameState.GameOver)
        {
            allowedToMove = false;
        }
    }
}
