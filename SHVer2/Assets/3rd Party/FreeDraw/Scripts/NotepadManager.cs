using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadManager : MonoBehaviour
{
    public static bool isUsingNotepad;
    private CameraManager cameraManager;
    private GameObject notepad;

    private void Awake()
    {
        notepad = gameObject.transform.GetChild(0).gameObject;
        cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
    }

    public void EnableNotepad()
    {
        isUsingNotepad = true;
        notepad.SetActive(true);
        cameraManager.SetCurrentCamera(CameraState.Notepad);
        cameraManager.SwitchCamera();
    }

    public void DisableNotepad()
    {
        isUsingNotepad = false;
        notepad.SetActive(false);
        cameraManager.SetCurrentCamera(cameraManager.GetDefaultCamera());
        cameraManager.SwitchCamera();
    }
}
