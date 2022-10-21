using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public bool stageUnlockable;
    public int unlockStage;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(MainMenu);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);

        if (stageUnlockable && unlockStage > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", unlockStage);
        }
    }
}
