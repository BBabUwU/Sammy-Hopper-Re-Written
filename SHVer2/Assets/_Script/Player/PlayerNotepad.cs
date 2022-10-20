using UnityEngine;
using System;

public class PlayerNotepad : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private bool isUsing;
    public static event Action TurnOnNotepad;
    public static event Action TurnOffNotepad;
    private PlayerWeapon playerWeapon;
    private bool canUse = true;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerWeapon = GetComponent<PlayerWeapon>();
    }
    public void SwitchToNotepad()
    {
        if (playerInput.NotepadButtonPressed() && isUsing && canUse)
        {
            isUsing = false;
            playerWeapon.allowedToFire = true;
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
            Actions.Switch_DefaultCamera?.Invoke();
            TurnOffNotepad?.Invoke();
            Actions.setInventory?.Invoke(true);
        }

        else if (playerInput.NotepadButtonPressed() && !isUsing && canUse)
        {
            isUsing = true;
            playerWeapon.allowedToFire = false;
            CanvasManager.Instance.SwitchCanvas(CanvasType.Notepad);
            Actions.Switch_Camera?.Invoke(CameraType.NotepadCamera);
            TurnOnNotepad?.Invoke();
            Actions.setInventory?.Invoke(false);
        }
    }

    private void SetNotepad(bool x)
    {
        canUse = x;
    }

    private void OnEnable()
    {
        Actions.setNotepad += SetNotepad;
        Actions.setAllControls += SetNotepad;
    }

    private void OnDisable()
    {
        Actions.setNotepad -= SetNotepad;
        Actions.setAllControls -= SetNotepad;
    }
}