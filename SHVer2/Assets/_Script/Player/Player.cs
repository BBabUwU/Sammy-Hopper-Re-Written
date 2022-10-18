using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRigidBody;
    private PlayerWeapon _playerWeapon;
    private PlayerParry _playerParry;
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
        _playerParry = GetComponent<PlayerParry>();
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

        //Inventory
        _playerInventory.OpenInventory();

        //Parry for stage 3
        _playerParry.PlayerParryFeature();
    }

    private Transform GetTransform()
    {
        return transform;
    }

    private void OnEnable()
    {
        Actions.playerPos += GetTransform;
    }

    private void OnDisable()
    {
        Actions.playerPos -= GetTransform;
    }
}
