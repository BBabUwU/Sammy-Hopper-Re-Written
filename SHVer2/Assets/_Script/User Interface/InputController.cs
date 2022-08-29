using UnityEngine;
using TMPro;
using System;

public enum AnswerType
{
    Answer1,
    Answer2
}

[RequireComponent(typeof(TMP_InputField))]
public class InputController : MonoBehaviour
{
    public AnswerType inputType;
    TMP_InputField inputButton;
    public static event Action<AnswerType, string> onValueChangedInput;
    private void Start()
    {
        inputButton = GetComponent<TMP_InputField>();
        inputButton.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(string _answer)
    {
        onValueChangedInput?.Invoke(inputType, _answer);
    }
}

