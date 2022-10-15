using UnityEngine;
using UnityEngine.UI;

public class ReturnMainMenu : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(MainMenu);
    }

    private void MainMenu()
    {
        Actions.GoMainMenu?.Invoke();
    }
}
