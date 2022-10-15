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
        Actions.setMovement?.Invoke(true);
        Actions.setWeapon?.Invoke(true);
        Actions.setNotepad?.Invoke(true);
        Actions.setInventory?.Invoke(true);
        GameManager.Instance.UpdateGameState(GameState.Exploration);
    }
}
