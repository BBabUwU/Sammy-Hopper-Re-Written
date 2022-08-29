using UnityEngine;
using System;

public class PlayerNotepad : MonoBehaviour
{
    private PlayerInput playerInput;

    //Events
    public static Action<CameraType> EnableNotepadCamera;
    public static Action SwitchDefault;

    [SerializeField] private bool isUsing;
    [SerializeField] private GameObject notepad;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    public void SwitchToNotepad()
    {
        if (playerInput.NotepadButtonPressed() && isUsing)
        {
            isUsing = false;
            notepad.SetActive(false);
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
            SwitchDefault?.Invoke();
        }

        else if (playerInput.NotepadButtonPressed() && !isUsing)
        {
            isUsing = true;
            notepad.SetActive(true);
            CanvasManager.Instance.SwitchCanvas(CanvasType.Notepad);
            EnableNotepadCamera?.Invoke(CameraType.NotepadCamera);
        }
    }
}