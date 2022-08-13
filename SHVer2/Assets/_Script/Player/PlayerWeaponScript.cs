using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] internal float fireDamage;
    [SerializeField] internal float fireSpeed;
    [SerializeField] internal float weaponCoolDownTime = 2f;
    [SerializeField] private bool allowedToFire = true;
    private PlayerInputScript _playerInput;
    private float nextFireTime = 0;
    private Animator _playerAnimation;

    private void Awake()
    {
        _playerScript = GetComponent<PlayerScript>();
        _playerInput = GetComponent<PlayerInputScript>();
        _playerAnimation = GetComponent<Animator>();
    }

    internal void FireWeapon()
    {
        if (!allowedToFire) return;

        if (_playerInput.WeaponFirePressed() && !WeaponOnCooldown())
        {
            ShootWeapon();
            _playerAnimation.SetTrigger("WeaponShoot");
            nextFireTime = Time.time + weaponCoolDownTime;
        }
    }

    private bool WeaponOnCooldown()
    {
        bool weaponOnCooldown = Time.time > nextFireTime ? false : true;
        return weaponOnCooldown;
    }

    internal void ShootWeapon()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
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
            allowedToFire = true;
        }

        if (state == GameState.AnsweringQuiz || state == GameState.InteractingNPC || state == GameState.GameOver)
        {
            allowedToFire = false;
        }
    }
}
