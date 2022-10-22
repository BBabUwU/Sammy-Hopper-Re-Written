using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInstruction : MonoBehaviour
{
    private void OnEnable()
    {
        Pause();
    }

    private void Start()
    {
        Actions.setAllControls?.Invoke(false);
        Actions.SetPause?.Invoke(false);
        Actions.setMovement?.Invoke(false);
        Actions.setInventory?.Invoke(false);
    }

    private bool gameIsPaused = false;
    private void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        Actions.setAllControls?.Invoke(true);
        Actions.setInventory?.Invoke(false);
        Actions.SetPause?.Invoke(true);
        Actions.setMovement?.Invoke(true);
        this.gameObject.SetActive(false);
    }
}
