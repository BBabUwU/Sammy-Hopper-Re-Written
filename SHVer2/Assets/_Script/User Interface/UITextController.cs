using UnityEngine;
using TMPro;

public enum UITextType
{
    QuestionText,
    DialogueText,
    NameText,
}

public class UITextController : MonoBehaviour
{
    [SerializeField] private UITextType textType;
    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    public string GetText(UITextType _type)
    {
        string currentText = null;

        if (textType == _type)
        {
            currentText = textUI.text;
            return currentText;
        }

        return currentText;
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
        InteractionDialogue.nameText += UpdateText;
        InteractionDialogue.dialogueText += UpdateText;
        InteractionDialogue.updateDialogueText += UpdateDialogue;
        InteractionDialogue.currentUIText += GetText;
        InteractionDialogue.updateUIText += UpdateText;

        ExplorationQuiz.UpdateQuestionText += UpdateText;

        BossQuiz.UpdateQuestionText += UpdateText;

        Actions.UpdateQuestionText += UpdateText;
    }

    private void OnDisable()
    {
        InteractionDialogue.nameText -= UpdateText;
        InteractionDialogue.dialogueText -= UpdateText;
        InteractionDialogue.updateDialogueText -= UpdateDialogue;
        InteractionDialogue.currentUIText -= GetText;
        InteractionDialogue.updateUIText -= UpdateText;

        ExplorationQuiz.UpdateQuestionText -= UpdateText;

        BossQuiz.UpdateQuestionText -= UpdateText;

        Actions.UpdateQuestionText -= UpdateText;
    }
}
