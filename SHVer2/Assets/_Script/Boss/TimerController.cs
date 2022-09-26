using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private Timer timer;

    private void Awake()
    {
        timer = GetComponent<Timer>();
    }

    private void SetTimer(TimerType timerType)
    {
        if (timerType == TimerType.collectTimer) StartGatherTimer();
        else if (timerType == TimerType.answerTimer) StartAnswerTimer();
    }

    private void StartGatherTimer()
    {
        timer.currentTime = timer.GatherTime;
        timer.timerType = TimerType.collectTimer;
        timer.enabled = true;
    }

    private void StartAnswerTimer()
    {
        timer.currentTime = timer.AnswerTime;
        timer.timerType = TimerType.answerTimer;
        timer.enabled = true;
    }

    private void OnEnable()
    {
        BossManager.startTimer += SetTimer;
    }

    private void OnDisable()
    {
        BossManager.startTimer -= SetTimer;
    }
}
