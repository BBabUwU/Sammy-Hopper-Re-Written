using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRigidBody;
    private PlayerWeapon _playerWeapon;
    private PlayerHealth _playerHealth;
    private PlayerUI _playerUI;
    private PlayerNotepad _playerNotepad;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerWeapon = GetComponent<PlayerWeapon>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerUI = GetComponent<PlayerUI>();
        _playerNotepad = GetComponent<PlayerNotepad>();
    }

    private void Start()
    {
        _playerUI.SetPlayerMaxHealth(_playerHealth.maxHealth);
    }

    private void FixedUpdate()
    {
        //This code was placed here because it deals with physics.
        _playerMovement.HorizontalMovement();
    }

    private void Update()
    {
        _playerHealth.IsDead();
        _playerWeapon.FireWeapon();
        _playerMovement.Jump();
        _playerMovement.FlipPlayer();

        //Animations
        _playerMovement.HorizontalMovementAnimation();

        //Notepad
        _playerNotepad.SwitchToNotepad();
    }
}
