using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum UIType
{
    PlayerUI,
    PlayerDeathUI,
    NotepadUI,
    DialogueUI,
    QuizUI,
    BossUI,
    QuizResult,
    VideoMenu,
    VideoPlayer
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    List<UIController> uiControllerList;
    private void Awake()
    {
        Instance = this;
        uiControllerList = GetComponentsInChildren<UIController>(true).ToList();
        DisableAllUI();
    }

    private void OnEnable()
    {
        Instance = this;
    }

    public void TurnOnUI(UIType _uiType)
    {
        UIController desiredUI = uiControllerList.Find(x => x.uiType == _uiType);

        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
        }
        else { Debug.Log(_uiType + " UI was not found"); }
    }

    public void TurnOffUI(UIType _uiType)
    {
        UIController desiredUI = uiControllerList.Find(x => x.uiType == _uiType);

        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(false);
        }
        else { Debug.Log(_uiType + " UI was not found"); }
    }

    public void DisableAllUI()
    {
        uiControllerList.ForEach(x => x.gameObject.SetActive(false));
    }
}
