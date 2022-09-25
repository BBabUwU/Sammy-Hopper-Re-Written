using UnityEngine;
using System;

public class PlayerNotepad : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private bool isUsing;
    private CameraSwitcher cameraSwitcher;
    public static event System.Action TurnOnNotepad;
    public static event System.Action TurnOffNotepad;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cameraSwitcher = GetComponent<CameraSwitcher>();
    }
    public void SwitchToNotepad()
    {
        if (playerInput.NotepadButtonPressed() && isUsing)
        {
            isUsing = false;
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
            cameraSwitcher.SwitchDefaultCamera();
            TurnOffNotepad?.Invoke();
        }

        else if (playerInput.NotepadButtonPressed() && !isUsing)
        {
            isUsing = true;
            CanvasManager.Instance.SwitchCanvas(CanvasType.Notepad);
            cameraSwitcher.SwitchCamera(CameraType.NotepadCamera);
            TurnOnNotepad?.Invoke();
        }
    }
}