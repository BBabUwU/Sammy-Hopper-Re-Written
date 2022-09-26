using UnityEngine;
using TMPro;
using System;

public enum TimerType
{
    collectTimer,
    answerTimer
}

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer settings")]
    public float GatherTime;
    public float AnswerTime;
    public float currentTime;
    public TimerType timerType;


    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;
    public static event Action<TimerType> timesUp;

    private void Awake()
    {
        currentTime = GatherTime;
    }

    void Update()
    {
        RunTimer();
    }

    private void RunTimer()
    {
        currentTime -= Time.deltaTime;

        if (hasLimit && currentTime <= timerLimit)
        {
            SetTimerText();
            timesUp?.Invoke(timerType);
            enabled = false;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }
}
