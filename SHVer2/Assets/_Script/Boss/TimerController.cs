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

    private void StartTimer()
    {
        timer.Init();
        timer.enabled = true;
    }

    private void StopTimer()
    {
        timer.enabled = false;
    }

    private void OnEnable()
    {
        BossManager.StartTimer += StartTimer;
        BossManager.StopTimer += StopTimer;
    }

    private void OnDisable()
    {
        BossManager.StartTimer -= StartTimer;
        BossManager.StopTimer -= StopTimer;
    }
}
