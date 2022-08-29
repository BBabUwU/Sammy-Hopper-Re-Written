
using UnityEngine;
using UnityEngine.UI;

public class ButtonCanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvasType;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        CanvasManager.Instance.SwitchCanvas(desiredCanvasType);
        GameManager.Instance.UpdateGameState(GameState.Exploration);
    }
}
