using UnityEngine;
using System;

public class CameraSwitcher : MonoBehaviour
{
    public static event Action<CameraType> Switch_Camera;
    public static event System.Action Switch_DefaultCamera;

    public void SwitchCamera(CameraType _cameraType)
    {
        Switch_Camera?.Invoke(_cameraType);
    }

    public void SwitchDefaultCamera()
    {
        Switch_DefaultCamera?.Invoke();
    }
}
