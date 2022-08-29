using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum UIType
{
    PlayerUI,
    NotepadUI,
    DialogueUI,
    QuizUI,
    BossUI
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    List<UIController> uiControllerList;
    private void Awake()
    {
        CreateSingleton();
        uiControllerList = GetComponentsInChildren<UIController>(true).ToList();
        DisableAllUI();
    }

    public void TurnOnUI(UIType _uiType)
    {
        UIController desiredUI = uiControllerList.Find(x => x.uiType == _uiType);

        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
        }
        else { Debug.Log("The desired UI was not found"); }
    }

    public void TurnOffUI(UIType _uiType)
    {
        UIController desiredUI = uiControllerList.Find(x => x.uiType == _uiType);

        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(false);
        }
        else { Debug.Log("The desired UI was not found"); }
    }

    public void DisableAllUI()
    {
        uiControllerList.ForEach(x => x.gameObject.SetActive(false));
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
