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
        currentTime = answerTime;
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
            timerText.color = Color.red;
            TimesUp?.Invoke();
            enabled = false;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }
}
