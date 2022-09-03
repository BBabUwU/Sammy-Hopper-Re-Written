using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class BossManager : MonoBehaviour
{
    [SerializeField] private int scoreWaitTime = 5;
    public static event Action StartPhase_1;
    public static event Action StartPhase_2;
    public static event Action<CameraType> SwitchBossArenaCamera;
    public static event Func<QuizScript> BossQuizScript;
    public static event Action<UITextType, string> DisplayText;
    public static event Action KillPlayer;
    public static event Action<int> SetPAttackMultiplier;

    private void StartPhase1()
    {
        StartPhase_1?.Invoke();
        SwitchBossArenaCamera?.Invoke(CameraType.BossArenaCamera);
    }

    private IEnumerator Phase1Passed()
    {
        ScoreMessage();
        yield return new WaitForSeconds(scoreWaitTime);

        UIManager.Instance.TurnOffUI(UIType.QuizResult);

        if (BossQuizScript().quiz.isPassed)
        {
            StartPhase2();
        }
        else { KillPlayer?.Invoke(); }
    }

    private void ScoreMessage()
    {
        string quizResult = string.Format("{0:0} / {1:0}", BossQuizScript().quiz.score, BossQuizScript().quiz.totalScore);

        DisplayText?.Invoke(UITextType.ScoreText, quizResult);

        string messageString = "You failed";

        if (BossQuizScript().quiz.isPassed)
        {
            messageString = "You passed";
        }

        DisplayText?.Invoke(UITextType.MessageText, messageString);
    }

    private void StartPhase2()
    {
        StartPhase_2?.Invoke();
        SetPAttackMultiplier?.Invoke(BossQuizScript().quiz.score);
        GameManager.Instance.UpdateGameState(GameState.BossBattle);
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.BossQuizEvaluation)
        {
            StartCoroutine(Phase1Passed());
        }
    }

    private void OnEnable()
    {
        TeleportToBoss.Teleported += StartPhase1;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDisable()
    {
        TeleportToBoss.Teleported -= StartPhase1;
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
}
