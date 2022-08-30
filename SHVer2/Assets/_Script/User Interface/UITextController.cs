using UnityEngine;
using TMPro;

public enum UITextType
{
    QuestionText,
    TimeText,
    DialogueText,
    NameText
}

public class UITextController : MonoBehaviour
{
    [SerializeField] private UITextType textType;
    private TMP_Text textUI;

    private void Awake()
    {
        textUI = GetComponent<TMP_Text>();
    }

    public string GetText(UITextType _type)
    {
        if (textType == _type)
        {
            string currentText = textUI.text;
            return currentText;
        }

        return null;
    }

    public void SetText(string _text)
    {
        textUI.text = _text;
    }

    private void UpdateText(UITextType _type, string _text)
    {
        if (textType == _type)
        {
            textUI.SetText(_text);
        }
    }

    private void UpdateDialogue(char c)
    {
        if (textType == UITextType.DialogueText)
        {
            textUI.text += c;
        }
    }

    private void OnEnable()
    {
        QuizScript.UpdateQuestionText += UpdateText;
        QuizScript.UpdateTimerText += UpdateText;
        InteractionDialogue.nameText += UpdateText;
        InteractionDialogue.dialogueText += UpdateText;
        InteractionDialogue.updateDialogueText += UpdateDialogue;
        InteractionDialogue.currentUIText += GetText;
        InteractionDialogue.updateUIText += UpdateText;
    }

    private void OnDisable()
    {
        QuizScript.UpdateQuestionText -= UpdateText;
        QuizScript.UpdateTimerText -= UpdateText;
        InteractionDialogue.nameText -= UpdateText;
        InteractionDialogue.dialogueText -= UpdateText;
        InteractionDialogue.updateDialogueText -= UpdateDialogue;
        InteractionDialogue.currentUIText -= GetText;
        InteractionDialogue.updateUIText -= UpdateText;
    }
}
