using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRigidBody;
    private PlayerWeapon _playerWeapon;
    private PlayerHealth _playerHealth;
    private PlayerNotepad _playerNotepad;
    private PlayerInventory _playerInventory;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerWeapon = GetComponent<PlayerWeapon>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerNotepad = GetComponent<PlayerNotepad>();
        _playerInventory = GetComponent<PlayerInventory>();
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

        //Video player
        _playerInventory.OpenVideoPlayer();
    }
}
