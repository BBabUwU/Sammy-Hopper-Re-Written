using UnityEngine;
using System;

public class PlayerNotepad : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private bool isUsing;
    private CameraSwitcher cameraSwitcher;
    public static event Action TurnOnNotepad;
    public static event Action TurnOffNotepad;
    private PlayerWeapon playerWeapon;
    private bool canUse = true;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerWeapon = GetComponent<PlayerWeapon>();
        cameraSwitcher = GetComponent<CameraSwitcher>();
    }
    public void SwitchToNotepad()
    {
        if (playerInput.NotepadButtonPressed() && isUsing && canUse)
        {
            isUsing = false;
            playerWeapon.allowedToFire = true;
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
            cameraSwitcher.SwitchDefaultCamera();
            TurnOffNotepad?.Invoke();
        }

        else if (playerInput.NotepadButtonPressed() && !isUsing && canUse)
        {
            isUsing = true;
            playerWeapon.allowedToFire = false;
            CanvasManager.Instance.SwitchCanvas(CanvasType.Notepad);
            cameraSwitcher.SwitchCamera(CameraType.NotepadCamera);
            TurnOnNotepad?.Invoke();
        }
    }

    private void SetNotepad(bool x)
    {
        canUse = x;
    }

    private void OnEnable()
    {
        Actions.setNotepad += SetNotepad;
    }

    private void OnDisable()
    {
        Actions.setNotepad -= SetNotepad;
    }
}