using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private float firePower;
    [SerializeField] internal float fireSpeed;
    [SerializeField] internal float weaponCoolDownTime = 2f;
    private PlayerInputScript _playerInput;
    private float nextFireTime = 0;

    private void Awake()
    {
        _playerScript = GetComponent<PlayerScript>();
        _playerInput = GetComponent<PlayerInputScript>();
    }

    internal void FireWeapon()
    {
        if (_playerInput.WeaponFirePressed() && !WeaponOnCooldown())
        {
            Debug.Log("shoot");
            ShootWeapon();
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
}
