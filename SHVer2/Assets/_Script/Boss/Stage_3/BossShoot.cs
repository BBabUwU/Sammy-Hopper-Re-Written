using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private GameObject quizProjPrefab;
    [SerializeField] private GameObject projPrefab;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private List<Transform> firePoints;
    [SerializeField] private List<Transform> meteorFirePoints;
    public float atkCoolDownTime = 3f;
    private float nextFireTime = 0;
    public bool canShoot = false;

    //Change attack pattern
    public float specialAtkCoolDownTime = 10f;
    public float nextSpecialAtkFireTime = 5f;
    private bool specialAtkIsActive = false;

    private void Update()
    {
        if (canShoot)
        {
            if (!AtkOnCooldown() && !specialAtkIsActive)
            {
                Normal_AttackPlayer();
                nextFireTime = Time.time + atkCoolDownTime;
            }

            if (!SpecialAtkOnCooldown())
            {
                if (!specialAtkIsActive)
                {
                    ShowerAttack();
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

    private void Normal_AttackPlayer()
    {
        int bulletType = UnityEngine.Random.Range(0, 2);

        if (bulletType == 0)
        {
            Instantiate(quizProjPrefab, firePoints[UnityEngine.Random.Range(0, firePoints.Count)].position, Quaternion.identity);
        }
        else if (bulletType == 1)
        {
            Instantiate(projPrefab, firePoints[UnityEngine.Random.Range(0, firePoints.Count)].position, Quaternion.identity);
        }
    }

    private void ShowerAttack()
    {
        int parryIndex = UnityEngine.Random.Range(0, meteorFirePoints.Count);

        for (int i = 0; i < meteorFirePoints.Count; i++)
        {
            GameObject mp = Instantiate(meteorPrefab, meteorFirePoints[i].position, Quaternion.identity);

            MeteorProjectile meteor = mp.GetComponent<MeteorProjectile>();

            if (i == parryIndex)
            {
                meteor.isParriable = true;
                meteor.renderer.color = Color.red;
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
