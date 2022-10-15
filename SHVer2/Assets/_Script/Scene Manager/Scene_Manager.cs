using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager Instance;

    private void Awake()
    {
        CreateSingleton();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
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
