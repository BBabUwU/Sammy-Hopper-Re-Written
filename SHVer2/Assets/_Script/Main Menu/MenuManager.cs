using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Scene_Manager sceneManager;
    List<MenuController> menuControllerList;
    MenuController lastActiveMenu;
    private void Awake()
    {

        CreateInstance();

        menuControllerList = GetComponentsInChildren<MenuController>(true).ToList();
        menuControllerList.ForEach(x => x.gameObject.SetActive(false));
    }

    private void Start()
    {
        SwitchMainMenu();
    }

    public void StartGame()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt");

        if (levelAt == 1) sceneManager.Load_Stage_1();
        if (levelAt == 2) sceneManager.Load_Stage_2();
        if (levelAt == 3) sceneManager.Load_Stage_3();
    }

    public void SwitchMainMenu()
    {
        SwitchMenu("Main_menu");
    }

    public void SwitchStageSelect()
    {
        SwitchMenu("Stage_select");
    }

    public void SwitchSettings()
    {
        SwitchMenu("Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void CreateInstance()
    {
        Instance = this;
    }

    public void SwitchMenu(string _type)
    {
        if (lastActiveMenu != null)
        {
            lastActiveMenu.gameObject.SetActive(false);
        }

        MenuController desiredMenu = menuControllerList.Find(x => x.menuType == _type);

        if (desiredMenu != null)
        {
            desiredMenu.gameObject.SetActive(true);
            lastActiveMenu = desiredMenu;
        }
        else { Debug.Log("The desired menu was not found"); }
    }
}
