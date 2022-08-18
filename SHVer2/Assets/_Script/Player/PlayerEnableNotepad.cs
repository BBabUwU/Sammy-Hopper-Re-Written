using UnityEngine;

public class PlayerEnableNotepad : MonoBehaviour
{
    private PlayerInput playerInput;
    private CameraManager cameraManager;
    [SerializeField] private NotepadManager notepadManager;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
    }

    public void SwitchToNotepad()
    {
        if (playerInput.NotepadButtonPressed() && !NotepadManager.isUsingNotepad)
        {
            notepadManager.EnableNotepad();
        }
        else if (playerInput.NotepadButtonPressed() && NotepadManager.isUsingNotepad)
        {
            notepadManager.DisableNotepad();
        }
    }
}