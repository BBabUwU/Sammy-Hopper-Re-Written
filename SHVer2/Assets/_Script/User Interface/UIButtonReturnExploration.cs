using UnityEngine;
using UnityEngine.UI;

public class UIButtonReturnExploration : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ReturnExploration);
    }

    private void ReturnExploration()
    {
        UIManager.Instance.DisableAllUI();
        GameManager.Instance.UpdateGameState(GameState.Exploration);
    }
}
