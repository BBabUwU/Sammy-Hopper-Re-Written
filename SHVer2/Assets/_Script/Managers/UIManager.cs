using UnityEngine;

public class UIManager : MonoBehaviour
{
    private CameraManager _cameraManager;
    [SerializeField] private GameObject _notepadUI;

    private void Awake()
    {
        _cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
    }

    private void Update()
    {
        EnableNotepadUI();
    }

    private void EnableNotepadUI()
    {
        if (_cameraManager.GetCurrentCamera() == CameraState.Notepad)
        {
            _notepadUI.SetActive(true);
        }
        else
        {
            _notepadUI.SetActive(false);
        }
    }
}
