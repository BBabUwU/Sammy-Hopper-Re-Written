using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stg2_BossAttack : MonoBehaviour
{
    public List<Transform> firePoints = new List<Transform>();
    public GameObject projPrefab;
    public bool BossTurn = false;
    public int fireNTimes;
    private int fireCount = 0;
    private int projectilesActive = 0;

    private void Update()
    {
        if (BossTurn)
        {
            if (fireCount < fireNTimes && projectilesActive == 0)
            {
                projectilesActive = 4;
                AttackPlayer();
                fireCount++;
            }
            else if (fireCount >= fireNTimes && projectilesActive == 0)
            {
                Debug.Log("Done");
                fireCount = 0;
                BossTurn = false;
                Actions.playerTurn?.Invoke();
            }
        }
    }

    private void decreaseProjectile()
    {
        projectilesActive -= 1;
    }

    private void AttackPlayer()
    {
        List<int> activeNumber = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int currentIndex = UnityEngine.Random.Range(0, firePoints.Count);

            while (activeNumber.Contains(currentIndex))
            {
                currentIndex = UnityEngine.Random.Range(0, firePoints.Count);
            }

            activeNumber.Add(currentIndex);

            Instantiate(projPrefab, firePoints[activeNumber[i]].position, Quaternion.identity);
        }
    }

    private void SetTurn()
    {
        BossTurn = true;
    }

    private void OnEnable()
    {
        Actions.decreaseProjectileCounter += decreaseProjectile;
        Actions.bossTurn += SetTurn;
    }

    private void OnDisable()
    {
        Actions.decreaseProjectileCounter -= decreaseProjectile;
        Actions.bossTurn -= SetTurn;
    }
}
