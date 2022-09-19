using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum CanvasType
{
    MainMenu,
    GameUI,
    Notepad,
    VideoPlayer,
    EndScreen
}

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    List<CanvasController> canvasControllerList;
    CanvasController lastActiveCanvas;
    private void Awake()
    {
        CreateSingleton();

        canvasControllerList = GetComponentsInChildren<CanvasController>(true).ToList();
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);

        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else { Debug.Log("The desired canvas was not found"); }
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
