using UnityEngine;
using TMPro;

public enum UITextType
{
    QuestionText,
    TimeText
}

public class UITextController : MonoBehaviour
{
    [SerializeField] private UITextType textType;
    private TMP_Text textUI;

    private void Awake()
    {
        textUI = GetComponent<TMP_Text>();
    }

    private void UpdateText(UITextType _type, string _text)
    {
        if (textType == _type)
        {
            textUI.SetText(_text);
        }
    }

    private void OnEnable()
    {
        QuizScript.UpdateQuestionText += UpdateText;
        QuizScript.UpdateTimerText += UpdateText;
    }

    private void OnDisable()
    {
        QuizScript.UpdateQuestionText -= UpdateText;
        QuizScript.UpdateTimerText -= UpdateText;
    }
}
