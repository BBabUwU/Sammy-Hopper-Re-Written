using UnityEngine;
using TMPro;
using System;

public enum AnswerType
{
    Answer1,
    Answer2
}

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldController : MonoBehaviour
{
    public AnswerType inputType;
    TMP_InputField inputButton;
    bool allowEnter;
    public static event Action<AnswerType, string> onValueChangeInput;
    public static event System.Action onSubmitAnswer;

    private void Start()
    {
        inputButton = GetComponent<TMP_InputField>();
        inputButton.onValueChanged.AddListener(OnValueChanged);
    }

    private void Update()
    {
        SubmitAnswerInput();
    }

    void OnValueChanged(string _answer)
    {
        onValueChangeInput?.Invoke(inputType, _answer);
    }

    private void SubmitAnswerInput()
    {
        //Checks if player is allowed to click enter, length of text is greater than 0 and return key (enter key) or numpad enter key is pressed.

        if ((allowEnter && (inputButton.text.Length > 0) && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))))
        {
            onSubmitAnswer?.Invoke();
            allowEnter = false;
        }
        else
        {
            allowEnter = inputButton.isFocused;
        }
    }

    private void ClearInputField()
    {
        inputButton.text = "";

        if (inputType == AnswerType.Answer1)
        {
            inputButton.Select();
        }
    }

    private void OnEnable()
    {
        ExplorationQuiz.ClearInput += ClearInputField;

        BossQuiz.ClearInput += ClearInputField;
    }

    private void OnDisable()
    {
        ExplorationQuiz.ClearInput -= ClearInputField;

        BossQuiz.ClearInput -= ClearInputField;
    }
}

