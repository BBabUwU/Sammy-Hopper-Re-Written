using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int unlockStage;
    private int nextSceneLoad;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(NextScene);
    }

    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void NextScene()
    {
        SceneManager.LoadScene(nextSceneLoad);

        if (unlockStage > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", unlockStage);
        }
    }
}
