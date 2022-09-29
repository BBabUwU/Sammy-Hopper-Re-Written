using System.Collections.Generic;
using UnityEngine;
using System;

public class BossManager : MonoBehaviour
{
    //Essence drop variables
    [Header("Essence prefab")]
    [SerializeField] private GameObject easyEssencePrefab;
    [SerializeField] private GameObject hardEssencePrefab;
    [SerializeField] private List<Transform> dropPoints = new List<Transform>();

    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Damage options")]
    [SerializeField] private int easyDamage = 20;
    [SerializeField] private int hardDamage = 40;

    //Enemies
    private int enemyCounter = 0;
    private int enemySpawnLimit = 7;
    private Boolean[] activeEnemyPosition = new Boolean[7];
    private GameObject[] activeEnemy = new GameObject[7];

    //Assigned reference to the instantiated object
    //Essence
    private GameObject easyEssence;
    private GameObject hardEssence;

    //Active positions
    //Essence
    private int activeEssencePosition_1;
    private int activeEssencePosition_2;

    //Delegate
    //Damage boss health
    public static event Action<int> damageBoss;

    //Stops the quiz
    public static event Action stopQuiz;

    //Destroys all enemies
    public static event Action killEnemy;

    //Start timer 
    public static event Action StartTimer;

    //Components
    private BossQuiz quiz;

    private void Awake()
    {
        quiz = GetComponent<BossQuiz>();
    }

    private void Start()
    {
        DropEasyEssence();
        DropHardEssence();
        StartTimer?.Invoke();
    }

    ///<summary>
    ///Functions for dropping essence
    ///</summary>

    private void DropEasyEssence()
    {

        activeEssencePosition_1 = RandomSpotIndex();

        while (activeEssencePosition_1 == activeEssencePosition_2)
        {
            activeEssencePosition_1 = RandomSpotIndex();
        }

        easyEssence = Instantiate(easyEssencePrefab, dropPoints[activeEssencePosition_1].position, Quaternion.identity);

        SpawnEnemy();
    }

    private void DropHardEssence()
    {
        activeEssencePosition_2 = RandomSpotIndex();

        while (activeEssencePosition_2 == activeEssencePosition_1)
        {
            activeEssencePosition_2 = RandomSpotIndex();
        }

        hardEssence = Instantiate(hardEssencePrefab, dropPoints[activeEssencePosition_2].position, Quaternion.identity);

        SpawnEnemy();
    }

    private int RandomSpotIndex()
    {
        return UnityEngine.Random.Range(0, dropPoints.Count);
    }

    ///<summary>
    ///Functions for Quiz
    ///</summary>

    private void CorrectAnswer(QuizDiff diff)
    {
        if (diff == QuizDiff.Easy)
        {
            DropEasyEssence();
            damageBoss?.Invoke(easyDamage);
        }

        else if (diff == QuizDiff.Hard)
        {
            DropHardEssence();
            damageBoss?.Invoke(hardDamage);
        }
    }

    private void StopQuiz()
    {
        stopQuiz?.Invoke();
    }

    ///<summary>
    ///Functions for enemy spawn
    ///</summary>

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

    private void CheckEnemyNull(int i)
    {
        if (activeEnemy[i] == null)
        {
            activeEnemyPosition[i] = false;
        }
    }

    private void EnemyKilled()
    {
        enemyCounter--;
    }

    ///<summary>
    ///Functions for timer
    ///</summary>

    private void TimesUp()
    {
        Destroy(easyEssence);
        Destroy(hardEssence);
        //killEnemy?.Invoke();
        StopQuiz();
    }


    ///<summary>
    ///Enable and Disable functions
    ///</summary>

    private void OnEnable()
    {
        BossQuiz.correctAnswer += CorrectAnswer;

        Timer.TimesUp += TimesUp;

        EnemyCounter.enemyKilled += EnemyKilled;
    }

    private void OnDisable()
    {
        BossQuiz.correctAnswer -= CorrectAnswer;

        Timer.TimesUp -= TimesUp;

        EnemyCounter.enemyKilled -= EnemyKilled;
    }

}
