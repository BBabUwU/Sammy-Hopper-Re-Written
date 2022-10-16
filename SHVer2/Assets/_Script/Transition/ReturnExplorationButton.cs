using UnityEngine;
using UnityEngine.UI;

public class ReturnExplorationButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ReturnExploration);
    }

    private void ReturnExploration()
    {
        Actions.setAllControls(true);
        GameManager.Instance.UpdateGameState(GameState.Exploration);
    }
}
