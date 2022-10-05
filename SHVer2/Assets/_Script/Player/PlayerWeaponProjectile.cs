using System.Collections;
using UnityEngine;

public class PlayerWeaponProjectile : MonoBehaviour
{
    private Rigidbody2D _projectileRigidbody;
    private GameObject _playerObj;
    private Player _playerScript;
    private PlayerWeapon _playerWeapon;
    private void Awake()
    {
        _projectileRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerWeapon = _playerObj.GetComponent<PlayerWeapon>();
        _playerScript = _playerObj.GetComponent<Player>();
        IgnoreLayers();
    }
    private void Start()
    {
        ProjectileTravel();
        StartCoroutine(SelfDestruct());
    }

    private void IgnoreLayers()
    {
        //Ignored collisions
        //12 = Bullet layer
        Physics2D.IgnoreLayerCollision(12, 11); //Item layer
        Physics2D.IgnoreLayerCollision(12, 13); //Npc layer
        Physics2D.IgnoreLayerCollision(12, 15); //Ignore layer
        Physics2D.IgnoreLayerCollision(12, 14); //Ignore Essence layer
        Physics2D.IgnoreLayerCollision(12, 16); //Ignore Interact Essence layer
    }

    private void ProjectileTravel()
    {
        _projectileRigidbody.velocity = transform.right * _playerWeapon.fireSpeed;
    }

    public float GetProjectileDamage()
    {
        return _playerWeapon.fireDamage;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage(_playerWeapon.fireDamage);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.Damage(_playerWeapon.fireDamage);
        }

        Destroy(gameObject);
    }
}
