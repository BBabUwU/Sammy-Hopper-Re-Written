using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Linq;

public enum CameraType
{
    PlayerCamera,
    NotepadCamera,
    BossArenaCamera
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
        PlayerNotepad.EnableNotepadCamera += SwitchCamera;
        PlayerNotepad.SwitchDefault += SwitchDefaultCamera;
    }

    private void OnDisable()
    {
        PlayerNotepad.EnableNotepadCamera -= SwitchCamera;
        PlayerNotepad.SwitchDefault -= SwitchDefaultCamera;
    }
}

