using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraState currentCameraState;
    private CameraState defaultCameraState;

    [Header("Camera components")]
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera notepadCamera;
    [SerializeField] private CinemachineVirtualCamera bossArenaCamera;


    private void ChangeStateToNotepad()
    {
        currentCameraState = CameraState.Notepad;
    }

    private void ChangeStateToBossCamera()
    {
        currentCameraState = CameraState.BossArena;
    }

    private void ChangeStateToDefaultCamera()
    {
        currentCameraState = defaultCameraState;
    }

    private void SetDefaultCamera(CameraState cameraState)
    {
        defaultCameraState = cameraState;
    }

    private void SetCameraPriotyToZero()
    {
        playerCamera.Priority = 0;
        notepadCamera.Priority = 0;
        bossArenaCamera.Priority = 0;
    }

    private void SwitchCamera()
    {
        SetCameraPriotyToZero();

        if (currentCameraState == CameraState.Player)
        {
            playerCamera.Priority = 1;
        }

        else if (currentCameraState == CameraState.Notepad)
        {
            notepadCamera.Priority = 1;
        }

        else if (currentCameraState == CameraState.BossArena)
        {
            bossArenaCamera.Priority = 1;
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////
    ///<Summary>
    //Event functions
    //Listening to game manager.
    ///</Summary>
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.BossBattle)
        {
            defaultCameraState = CameraState.BossArena;
            currentCameraState = CameraState.BossArena;
        }

        else
        {
            defaultCameraState = CameraState.Player;
            currentCameraState = CameraState.Player;
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

        //Enable Notepad
        PlayerNotepad.OnNotepadEnabled += ChangeStateToNotepad;
        PlayerNotepad.OnNotepadEnabled += SwitchCamera;

        //Disable Notepad
        PlayerNotepad.OnNotepadDisabled += ChangeStateToDefaultCamera;
        PlayerNotepad.OnNotepadDisabled += SwitchCamera;

        //Boss Camera
        BossManager.BossArenaCamera += ChangeStateToBossCamera;
        BossManager.BossArenaCamera += SwitchCamera;
        BossManager.ChangeDefaultCamera += SetDefaultCamera;

    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;

        //Notepad
        PlayerNotepad.OnNotepadEnabled -= ChangeStateToNotepad;
        PlayerNotepad.OnNotepadEnabled -= SwitchCamera;

        //Disable Notepad
        PlayerNotepad.OnNotepadDisabled -= ChangeStateToDefaultCamera;
        PlayerNotepad.OnNotepadDisabled -= SwitchCamera;

        //Boss Camera
        BossManager.BossArenaCamera -= ChangeStateToBossCamera;
        BossManager.BossArenaCamera -= SwitchCamera;
        BossManager.ChangeDefaultCamera -= SetDefaultCamera;

    }
}

public enum CameraState { Player, Notepad, BossArena }