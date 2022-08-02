using UnityEngine;

public class ChangeDefaultCamera : MonoBehaviour
{
    private CameraManager _cameraManager;

    private void Awake()
    {
        _cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_cameraManager.GetDefaultCamera() == CameraState.Player)
            {
                _cameraManager.SetCurrentCamera(CameraState.BossArena);
                _cameraManager.SetDefaultCamera(CameraState.BossArena);
            }

            else
            {
                _cameraManager.SetCurrentCamera(CameraState.Player);
                _cameraManager.SetDefaultCamera(CameraState.Player);
            }
        }
    }
}
