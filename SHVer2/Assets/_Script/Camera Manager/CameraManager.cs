using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Linq;

public enum CameraType
{
    PlayerCamera,
    NotepadCamera,
    Graphing
}

public class CameraManager : MonoBehaviour
{
    public CameraType dCamera;
    CameraController defaultCamera;
    CameraController lastActiveCamera;
    List<CameraController> cameraControllerList;

    private void Awake()
    {
        cameraControllerList = GetComponentsInChildren<CameraController>().ToList();
    }

    private void Start()
    {
        SetDefaultCamera(dCamera);
        SwitchCamera(CameraType.PlayerCamera);
    }

    public void SetDefaultCamera(CameraType _cameraType)
    {
        defaultCamera = cameraControllerList.Find(x => x.cameraType == _cameraType);
    }

    public void SwitchCamera(CameraType _cameraType)
    {
        if (lastActiveCamera != null)
        {
            lastActiveCamera.virtualCamera.Priority = 0;
        }

        CameraController desiredCamera = cameraControllerList.Find(x => x.cameraType == _cameraType);

        if (desiredCamera != null)
        {
            desiredCamera.virtualCamera.Priority = 1;
            lastActiveCamera = desiredCamera;
        }
        else { Debug.Log("The desired camera was not found"); }
    }

    public void SwitchDefaultCamera()
    {
        if (lastActiveCamera != null)
        {
            lastActiveCamera.virtualCamera.Priority = 0;
        }

        defaultCamera.virtualCamera.Priority = 1;
    }

    private void OnEnable()
    {
        Actions.Switch_Camera += SwitchCamera;
        Actions.Switch_DefaultCamera += SwitchDefaultCamera;
        Actions.Set_DefaultCamera += SetDefaultCamera;
    }

    private void OnDisable()
    {
        Actions.Switch_Camera -= SwitchCamera;
        Actions.Switch_DefaultCamera -= SwitchDefaultCamera;
        Actions.Set_DefaultCamera -= SetDefaultCamera;
    }
}