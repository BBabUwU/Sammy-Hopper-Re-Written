using UnityEngine;

public class PlayerEnableNotepadScript : MonoBehaviour
{
    private PlayerInputScript _playerInput;
    private CameraManager _cameraManager;
    [SerializeField] private GameObject _notepad;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInputScript>();
        _cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
    }

    internal void SwitchToNotepad()
    {
        if (_playerInput.NotepadButtonPressed() && !EnableNotepad.isUsingNotepad)
        {
            EnableNotepad.isUsingNotepad = true;
            _notepad.SetActive(true);
            _cameraManager.SetCurrentCamera(CameraState.Notepad);
        }
        else if (_playerInput.NotepadButtonPressed() && EnableNotepad.isUsingNotepad)
        {
            EnableNotepad.isUsingNotepad = false;
            _notepad.SetActive(false);
            _cameraManager.SetCurrentCamera(_cameraManager.GetDefaultCamera());
        }
    }
}