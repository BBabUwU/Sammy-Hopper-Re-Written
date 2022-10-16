using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    private Player _playerScript;
    public int fireDamage;
    public float fireSpeed;
    public float weaponCoolDownTime = 2f;
    public bool allowedToFire = true;
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
            Actions.playAudio?.Invoke("Fire");
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

    private void SetWeapon(bool x)
    {
        allowedToFire = x;
    }

    //Event functions
    //Listening to game manager.
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        Actions.setWeapon += SetWeapon;
        Actions.setAllControls += SetWeapon;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        Actions.setWeapon -= SetWeapon;
        Actions.setAllControls -= SetWeapon;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Exploration || state == GameState.BossBattle)
        {
            allowedToFire = true;
        }

        else
        {
            allowedToFire = false;
        }
    }
}
