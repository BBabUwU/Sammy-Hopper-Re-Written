using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    private QuizScript quizScript;
    private BossHealth bossHealth;
    private Animator bossAnimator;
    [SerializeField] private TMP_Text scoreResult;
    [SerializeField] private TMP_Text passMessage;

    //Events
    public static event Action BossArenaCamera;
    public static event Action<CameraState> ChangeDefaultCamera;

    private void Awake()
    {
        quizScript = boss.GetComponent<QuizScript>();
        bossHealth = boss.GetComponent<BossHealth>();
        bossAnimator = boss.GetComponent<Animator>();
    }

    private void SetPlayerAttackMultiplier()
    {
        bossHealth.SetAttackMultiplier(quizScript.quiz.score);
    }

    private void SetBossToActive()
    {
        boss.SetActive(true);
        quizScript.enabled = true;
    }

    private IEnumerator StartBossFight()
    {
        yield return new WaitForSeconds(5f);
        if (!quizScript.quiz.isPassed)
        {
            Debug.Log("DIEDED");
        }
        else
        {
            SetPlayerAttackMultiplier();
            bossAnimator.SetBool("BossStarted", true);
            GameManager.Instance.UpdateGameState(GameState.BossBattle);
        }
    }

    private void SwitchCamera()
    {
        BossArenaCamera?.Invoke();
        ChangeDefaultCamera?.Invoke(CameraState.BossArena);
    }

    private void ScoreMessage()
    {
        if (quizScript.quiz.isPassed)
        {
            scoreResult.text = string.Format("{0:0} / {1:0}", quizScript.quiz.score, quizScript.quiz.totalScore);
            passMessage.text = "You passed (Away)";
        }
        else if (!quizScript.quiz.isPassed)
        {
            scoreResult.text = string.Format("{0:0} / {1:0}", quizScript.quiz.score, quizScript.quiz.totalScore);
            passMessage.text = "You stupid";
        }

        StartCoroutine(StartBossFight());
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.ShowScore)
        {
            ScoreMessage();
        }
    }

    private void OnEnable()
    {
        TeleportToBoss.Teleported += SwitchCamera;
        TeleportToBoss.Teleported += SetBossToActive;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDisable()
    {
        TeleportToBoss.Teleported -= SwitchCamera;
        TeleportToBoss.Teleported -= SetBossToActive;
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
}
