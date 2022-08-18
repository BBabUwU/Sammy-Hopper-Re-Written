using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    private Player _playerScript;
    public float fireDamage;
    public float fireSpeed;
    public float weaponCoolDownTime = 2f;
    [SerializeField] private bool allowedToFire = true;
    private PlayerInput _playerInput;
    private float nextFireTime = 0;
    private Animator _playerAnimation;

    private void Awake()
    {
        _playerScript = GetComponent<Player>();
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimation = GetComponent<Animator>();
    }

    public void FireWeapon()
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

    public void ShootWeapon()
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

        if (state == GameState.AnsweringQuiz || state == GameState.NPCInteraction || state == GameState.GameOver)
        {
            allowedToFire = false;
        }
    }
}