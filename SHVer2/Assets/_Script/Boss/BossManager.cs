using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

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

    [Header("Level Complete UI")]
    [SerializeField] private TextMeshProUGUI easyCounter;
    [SerializeField] private TextMeshProUGUI hardCounter;

    //Tracks the number of essence type answered
    private int easyEssenceCounter = 0;
    private int hardEssenceCounter = 0;

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
    //Message boss turn
    public static event Action<Boolean> bossTurn;

    //Damage boss health
    public static event Action<int> damageBoss;

    //Stops the quiz
    public static event Action stopQuiz;

    //Destroys all enemies
    public static event Action killEnemy;

    //timer 
    public static event Action StartTimer;
    public static event Action StopTimer;

    private void Start()
    {
        StartAnswerPhase();
    }

    ///<summary>
    ///Starts answer phase
    ///</summary>
    private void StartAnswerPhase()
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
            easyEssenceCounter++;
            DropEasyEssence();
            damageBoss?.Invoke(easyDamage);
        }

        else if (diff == QuizDiff.Hard)
        {
            hardEssenceCounter++;
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

    private void SpawnAllEnemies()
    {
        for (int i = 0; i < activeEnemy.Length; i++)
        {
            SpawnEnemy();
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
        StopQuiz();
        SpawnAllEnemies();
        bossTurn?.Invoke(true);
    }

    ///<summary>
    ///Functions for UI
    ///</summary>

    private void DisplayResult()
    {
        easyCounter.text = easyEssenceCounter.ToString();
        hardCounter.text = hardEssenceCounter.ToString();
    }

    ///<summary>
    ///Functions for Boss Defeated
    ///</summary>

    private void BossDefeated()
    {
        stopQuiz?.Invoke();
        StopTimer?.Invoke();
        killEnemy?.Invoke();
        DisplayResult();
        StartCoroutine(DelayChangeState());
    }

    private IEnumerator DelayChangeState()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.UpdateGameState(GameState.BossDefeated);
    }


    ///<summary>
    ///Enable and Disable functions
    ///</summary>

    private void OnEnable()
    {
        BossQuiz.correctAnswer += CorrectAnswer;

        Timer.TimesUp += TimesUp;

        EnemyCounter.enemyKilled += EnemyKilled;

        BossAttack.TurnOver += StartAnswerPhase;

        BossHealth.BossDefeated += BossDefeated;
    }

    private void OnDisable()
    {
        BossQuiz.correctAnswer -= CorrectAnswer;

        Timer.TimesUp -= TimesUp;

        EnemyCounter.enemyKilled -= EnemyKilled;

        BossAttack.TurnOver -= StartAnswerPhase;

        BossHealth.BossDefeated -= BossDefeated;
    }

}
