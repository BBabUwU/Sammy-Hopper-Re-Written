using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager Instance;

    private void Awake()
    {
        CreateSingleton();
    }

    public void LoadScene(ButtonType _buttonType)
    {
        GameManager.Instance.UpdateGameState(GameState.SceneLoad);
        SceneManager.LoadScene(1);
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
        UIButtonController.Button_Clicked += LoadScene;
    }

    private void OnDisable()
    {
        UIButtonController.Button_Clicked -= LoadScene;
    }
}
