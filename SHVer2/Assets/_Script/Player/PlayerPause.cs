using UnityEngine;
using UnityEngine.UI;

public class PlayerPause : MonoBehaviour
{
    private bool gameIsPaused = false;
    private bool canPause = true;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void PauseState()
    {
        if (playerInput.PauseButton() & canPause)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void SetPause(bool x)
    {
        canPause = x;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        CanvasManager.Instance.SwitchCanvas(CanvasType.PauseMenu);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
    }

    private void OnEnable()
    {
        Actions.SetPause += SetPause;
    }

    private void OnDisable()
    {
        Actions.SetPause += SetPause;
    }
}
