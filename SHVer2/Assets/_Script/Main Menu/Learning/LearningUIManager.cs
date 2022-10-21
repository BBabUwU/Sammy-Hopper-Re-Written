using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LearningUIManager : MonoBehaviour
{
    public static LearningUIManager Instance;
    List<LearningUIController> learningUIControllerList;
    LearningUIController lastActiveMenu;


    private void Awake()
    {
        CreateInstance();

        learningUIControllerList = GetComponentsInChildren<LearningUIController>(true).ToList();
        learningUIControllerList.ForEach(x => x.gameObject.SetActive(false));
    }

    private void CreateInstance()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Options();
    }

    //Switch Phase
    public void Options()
    {
        SwitchOption("options");
    }

    public void SwitchVideoOption()
    {
        SwitchOption("switchVideo");
    }

    public void SwitchExtraOption()
    {
        SwitchOption("switchExtra");
    }

    //Videos menu
    public void SwitchStage1Video()
    {
        SwitchOption("stage1vid");
    }

    public void SwitchStage2Video()
    {
        SwitchOption("stage2vid");
    }

    public void SwitchStage3Video()
    {
        SwitchOption("stage3vid");
    }

    //Extra menus
    public void SwitchStage1Extra()
    {
        SwitchOption("stage1ex");
    }

    public void SwitchStage2Extra()
    {
        SwitchOption("stage2ex");
    }

    public void SwitchStage3Extra()
    {
        SwitchOption("stage3ex");
    }


    public void SwitchOption(string _type)
    {
        if (lastActiveMenu != null)
        {
            lastActiveMenu.gameObject.SetActive(false);
        }

        LearningUIController desiredMenu = learningUIControllerList.Find(x => x.optionState == _type);

        if (desiredMenu != null)
        {
            desiredMenu.gameObject.SetActive(true);
            lastActiveMenu = desiredMenu;
        }
        else { Debug.Log("The desired menu was not found"); }
    }
}
