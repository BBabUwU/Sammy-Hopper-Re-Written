using UnityEngine;
using UnityEngine.UI;

public class NextSceneButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(NextScene);
    }

    private void NextScene()
    {
        Actions.GoNextScene?.Invoke();
    }
}
