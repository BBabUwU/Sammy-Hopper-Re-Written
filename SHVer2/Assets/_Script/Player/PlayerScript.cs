using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private PlayerMovementScript _playerMovement;
    private Rigidbody2D _playerRigidBody;
    private PlayerWeaponScript _playerWeapon;
    private PlayerHealthScript _playerHealth;
    private PlayerUIScript _playerUI;
    private PlayerEnableNotepadScript _playerNotepad;

    private void Awake()
    {
        print("Player script awaken");
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovementScript>();
        _playerWeapon = GetComponent<PlayerWeaponScript>();
        _playerHealth = GetComponent<PlayerHealthScript>();
        _playerUI = GetComponent<PlayerUIScript>();
        _playerNotepad = GetComponent<PlayerEnableNotepadScript>();
    }

    private void Start()
    {
        _playerUI.SetPlayerMaxHealth(_playerHealth.maxHealth);
    }

    private void FixedUpdate()
    {
        //This code was placed here because it deals with physics.
        _playerMovement.PLayerHorizontalMovement();
    }

    private void Update()
    {
        _playerHealth.PlayerIsDead();
        _playerWeapon.FireWeapon();
        _playerMovement.PlayerJump();
        _playerMovement.FlipPlayer();
        _playerNotepad.SwitchToNotepad();

        //Animations
        _playerMovement.PlayerHorizontalMovementAnimation();
    }



}
