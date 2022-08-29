using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CameraType cameraType;
    [HideInInspector] public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
}
