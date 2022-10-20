using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private GameObject parriableProjPrefab;
    [SerializeField] private GameObject nonParriableProjPrefab;
    float randomTime;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private Sprite parryMeteor;
    [SerializeField] private Sprite[] damageMeteor;
    [SerializeField] private List<Transform> firePoints;
    [SerializeField] private List<Transform> meteorFirePoints;
    public float atkCoolDownTime = 3f;
    private float nextFireTime = 0;
    public bool canShoot = false;
    public bool canContinueShooting = true;

    //Change attack pattern
    public float specialAtkCoolDownTime = 10f;
    public float nextSpecialAtkFireTime = 5f;
    private bool specialAtkIsActive = false;

    private void Update()
    {
        if (canShoot)
        {
            if (canContinueShooting && canShoot && !specialAtkIsActive)
            {

                randomTime = Random.Range(1f, 3f);
                StartCoroutine(AttackPlayer());
                canContinueShooting = false;
            }

            if (!SpecialAtkOnCooldown())
            {
                if (!specialAtkIsActive)
                {
                    MeteorAttack();
                    nextSpecialAtkFireTime = Time.time + specialAtkCoolDownTime;
                    specialAtkIsActive = true;
                }
            }
        }
    }

    private void SpecialAtkInactive()
    {
        specialAtkIsActive = false;
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(randomTime);
        Actions.shootAnim?.Invoke();

        int spawnIndex = UnityEngine.Random.Range(0, firePoints.Count);
        int typeIndex = UnityEngine.Random.Range(0, 2);

        if (typeIndex == 0)
        {
            Instantiate(parriableProjPrefab, firePoints[spawnIndex].position, Quaternion.identity);
        }
        else if (typeIndex == 1)
        {
            Instantiate(nonParriableProjPrefab, firePoints[spawnIndex].position, Quaternion.identity);
        }

        canContinueShooting = true;
    }

    private void MeteorAttack()
    {
        Actions.meteorShootAnim?.Invoke();
        int parryIndex = UnityEngine.Random.Range(0, meteorFirePoints.Count);

        for (int i = 0; i < meteorFirePoints.Count; i++)
        {
            GameObject mp = Instantiate(meteorPrefab, meteorFirePoints[i].position, Quaternion.identity);

            MeteorProjectile meteor = mp.GetComponent<MeteorProjectile>();

            if (i == parryIndex)
            {
                meteor.isParriable = true;
                meteor._renderer.sprite = parryMeteor;
            }
            else
            {
                meteor.isParriable = false;
                meteor._renderer.sprite = damageMeteor[UnityEngine.Random.Range(0, 3)];
            }
        }
    }

    private bool AtkOnCooldown()
    {
        bool atkOnCooldown = Time.time > nextFireTime ? false : true;
        return atkOnCooldown;
    }

    private bool SpecialAtkOnCooldown()
    {
        bool atkOnCooldown = Time.time > nextSpecialAtkFireTime ? false : true;
        return atkOnCooldown;
    }

    private void OnEnable()
    {
        Actions.meteorAtkOver += SpecialAtkInactive;
    }

    private void OnDisable()
    {
        Actions.meteorAtkOver -= SpecialAtkInactive;
    }
}
