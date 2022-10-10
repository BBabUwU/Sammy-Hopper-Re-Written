using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stg3_BossManager : MonoBehaviour
{
    [HideInInspector] public BossShoot shootManager;
    public int parryLimit;
    public int parryCounter = 0;

    private void Awake()
    {
        shootManager = GetComponent<BossShoot>();
    }

    private void Start()
    {
        Actions.disableParry(false);
        StartParryPhase();
    }

    private void StartQuiz()
    {
        shootManager.canShoot = false;
    }

    private void StartParryPhase()
    {
        Actions.switchShield?.Invoke(true);
        parryCounter = 0;
        shootManager.canShoot = true;
    }

    private void AnswerCorrect()
    {
        Actions.switchShield?.Invoke(false);
        shootManager.canShoot = false;
    }

    private void IncreaseParryCounter()
    {
        parryCounter++;

        if (parryCounter > parryLimit)
        {
            parryCounter = parryLimit;
        }
    }

    public bool CanAnswer()
    {
        return parryCounter == parryLimit;
    }

    private void OnEnable()
    {
        Actions.parried += IncreaseParryCounter;
        Actions.damageLimitReached += StartParryPhase;
        Actions.correctAnswer += AnswerCorrect;
        Actions.inCorrect += StartParryPhase;
    }

    private void OnDisable()
    {
        Actions.parried -= IncreaseParryCounter;
        Actions.damageLimitReached -= StartParryPhase;
        Actions.correctAnswer -= AnswerCorrect;
        Actions.inCorrect -= StartParryPhase;
    }
}
