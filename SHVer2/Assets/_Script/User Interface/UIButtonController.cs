using UnityEngine;
using UnityEngine.UI;
using System;

public enum ButtonType
{
    StartButton
}

public class UIButtonController : MonoBehaviour
{
    public ButtonType buttonType;
    private Button button;
    public static event Action<ButtonType> Button_Clicked;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Button_Clicked?.Invoke(buttonType);
    }
}
