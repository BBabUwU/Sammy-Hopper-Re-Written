using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProj : MonoBehaviour
{
    [SerializeField] private GameObject quizProjPrefab;
    [SerializeField] private List<Transform> firePoints;
    public float atkCoolDownTime = 3f;
    private float nextFireTime = 0;
    private bool canShoot = true;
    public int parryCounter = 0;

    private void Update()
    {
        if (canShoot && !AtkOnCooldown() && parryCounter != 3)
        {
            AttackPlayer();
            nextFireTime = Time.time + atkCoolDownTime;
        }
    }

    private void AttackPlayer()
    {
        int spawnIndex = UnityEngine.Random.Range(0, firePoints.Count);

        Instantiate(quizProjPrefab, firePoints[spawnIndex].position, Quaternion.identity);
    }

    private bool AtkOnCooldown()
    {
        bool atkOnCooldown = Time.time > nextFireTime ? false : true;
        return atkOnCooldown;
    }
}
