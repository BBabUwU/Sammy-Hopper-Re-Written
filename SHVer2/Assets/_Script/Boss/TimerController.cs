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
        timer.enabled = true;
    }
}
