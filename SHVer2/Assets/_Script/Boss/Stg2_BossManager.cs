using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stg2_BossManager : MonoBehaviour
{
    [SerializeField] private List<Transform> dropPoints = new List<Transform>();
    [SerializeField] private GameObject enemyPrefab;
    //Enemies
    private int enemyCounter = 0;
    private int enemySpawnLimit = 4;
    private Boolean[] activeEnemyPosition = new Boolean[4];
    private GameObject[] activeEnemy = new GameObject[4];

    private void Start()
    {
        SpawnEnemy();
        Actions.startTime?.Invoke();
    }

    private void PlayerWon()
    {
        KillAllEnemy();
        Actions.stopQuiz?.Invoke();
        Actions.stopTime?.Invoke();
    }

    private int RandomSpotIndex()
    {
        return UnityEngine.Random.Range(0, dropPoints.Count);
    }

    private void SpawnEnemy()
    {
        int positionIndex = RandomSpotIndex();

        if (enemyCounter < enemySpawnLimit)
        {
            while (activeEnemyPosition[positionIndex] == true)
            {
                positionIndex = RandomSpotIndex();
                CheckEnemyNull(positionIndex);
            }

            activeEnemy[positionIndex] = Instantiate(enemyPrefab, dropPoints[positionIndex].position, Quaternion.identity);

            activeEnemyPosition[positionIndex] = true;
            enemyCounter++;
        }
        else
        {
            Debug.Log("Spawn limit reached");
        }
    }

    private void KillAllEnemy()
    {
        foreach (var enemy in activeEnemy)
        {
            if (enemy != null) Destroy(enemy);
        }
    }

    private void EnemySlain()
    {
        enemyCounter--;
    }

    private void CheckEnemyNull(int i)
    {
        if (activeEnemy[i] == null)
        {
            activeEnemyPosition[i] = false;
        }
    }

    private void PlayerTurn()
    {
        Actions.startQuiz?.Invoke();
        Actions.startTime?.Invoke();
    }

    private void TimesUp()
    {
        Actions.stopQuiz?.Invoke();
        Actions.bossTurn?.Invoke();
    }

    private void OnEnable()
    {
        Actions.spawnEnemy += SpawnEnemy;
        Actions.decreaseEnemyCounter += EnemySlain;
        Actions.timesUp += TimesUp;
        Actions.playerTurn += PlayerTurn;
        Actions.bossDefeated += PlayerWon;
    }

    private void OnDisable()
    {
        Actions.spawnEnemy -= SpawnEnemy;
        Actions.decreaseEnemyCounter -= EnemySlain;
        Actions.timesUp -= TimesUp;
        Actions.playerTurn -= PlayerTurn;
        Actions.bossDefeated -= PlayerWon;
    }
}
