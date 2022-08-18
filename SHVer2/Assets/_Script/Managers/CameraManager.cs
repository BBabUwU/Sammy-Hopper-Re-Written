using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraState currentCamera;
    private CameraState defaultCameraState;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera notepadCamera;
    [SerializeField] private CinemachineVirtualCamera bossArenaCamera;

    private void Awake()
    {
        currentCamera = CameraState.Player;
        defaultCameraState = CameraState.Player;
    }

    public void SetCurrentCamera(CameraState currentCamera)
    {
        this.currentCamera = currentCamera;
    }

    public void SetDefaultCamera(CameraState newDefaultCamera)
    {
        defaultCameraState = newDefaultCamera;
    }

    public CameraState GetCurrentCamera()
    {
        return currentCamera;
    }

    public CameraState GetDefaultCamera()
    {
        return defaultCameraState;
    }

    public void SwitchCamera()
    {
        SetCameraPriotyToZero();

        if (currentCamera == CameraState.Player)
        {
            playerCamera.Priority = 1;
        }

        else if (currentCamera == CameraState.Notepad)
        {
            notepadCamera.Priority = 1;
        }
        else if (currentCamera == CameraState.BossArena)
        {
            bossArenaCamera.Priority = 1;
        }
    }

    private void SetCameraPriotyToZero()
    {
        playerCamera.Priority = 0;
        notepadCamera.Priority = 0;
        bossArenaCamera.Priority = 0;
    }

    //Event functions
    //Listening to game manager.
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.BossBattle)
        {
            SetDefaultCamera(CameraState.BossArena);
            SetCurrentCamera(CameraState.BossArena);
            SwitchCamera();
        }

        else
        {
            SetDefaultCamera(CameraState.Player);
            SetCurrentCamera(CameraState.Player);
            SwitchCamera();
        }
    }
}

public enum CameraState { Player, Notepad, BossArena }