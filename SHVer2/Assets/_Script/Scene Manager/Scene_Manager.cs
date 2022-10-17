using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Load_Stage_1()
    {
        SceneManager.LoadScene(2);
    }

    public void Load_Stage_2()
    {
        SceneManager.LoadScene(5);
    }

    public void Load_Stage_3()
    {
        SceneManager.LoadScene(8);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        Actions.GoNextScene += LoadNextScene;
        Actions.GoMainMenu += LoadMainMenu;
    }

    private void OnDisable()
    {
        Actions.GoNextScene -= LoadNextScene;
        Actions.GoMainMenu -= LoadMainMenu;
    }
}
