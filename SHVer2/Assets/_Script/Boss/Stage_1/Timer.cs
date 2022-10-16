using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer settings")]
    public float answerTime;
    [HideInInspector] public float currentTime;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    //Delegate
    //Sends message that the timer has reached its limit.
    public static event Action TimesUp;

    private void Awake()
    {
        Init();
    }

    void Update()
    {
        RunTimer();
    }

    public void Init()
    {
        currentTime = answerTime;
    }

    private void RunTimer()
    {
        currentTime -= Time.deltaTime;

        if (hasLimit && currentTime <= timerLimit)
        {
            SetTimerText();
            TimesUp?.Invoke();
            Actions.timesUp?.Invoke();
            enabled = false;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }

    private float GetTime()
    {
        return currentTime;
    }

    private void OnEnable()
    {
        Actions.timeLeft += GetTime;
    }

    private void OnDisable()
    {
        Actions.timeLeft -= GetTime;
    }
}
