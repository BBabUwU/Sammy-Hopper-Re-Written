using System.Collections;
using UnityEngine;

public class PlayerWeaponProjectile : MonoBehaviour
{
    private Rigidbody2D _projectileRigidbody;
    private GameObject _playerObj;
    private PlayerScript _playerScript;
    private PlayerWeaponScript _playerWeaponScript;
    private void Awake()
    {
        _projectileRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerWeaponScript = _playerObj.GetComponent<PlayerWeaponScript>();
        _playerScript = _playerObj.GetComponent<PlayerScript>();
    }
    private void Start()
    {
        ProjectileTravel();
        StartCoroutine(SelfDestruct());
    }
    private void ProjectileTravel()
    {
        _projectileRigidbody.velocity = transform.right * _playerWeaponScript.fireSpeed;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
