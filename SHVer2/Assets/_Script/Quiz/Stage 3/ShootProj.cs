using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProj : MonoBehaviour
{
    [SerializeField] private GameObject parriableProjPrefab;
    [SerializeField] private GameObject nonParriableProjPrefab;
    [SerializeField] private List<Transform> firePoints;
    private bool canShoot = true;
    public int parryCounter = 0;
    float randomTime;

    private void Update()
    {
        if (canShoot && parryCounter != 3)
        {
            randomTime = Random.Range(1f, 6f);
            StartCoroutine(AttackPlayer());
            canShoot = false;
        }
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(randomTime);

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

        canShoot = true;
    }
}
