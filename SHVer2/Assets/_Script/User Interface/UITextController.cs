using UnityEngine;
using TMPro;

public enum UITextType
{
    QuestionText_1,
    QuestionText_2,
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
            AudioManager.Instance.Play("voice_light");
            textUI.text += c;
        }
    }

    private void OnEnable()
    {
        Actions.nameText += UpdateText;
        Actions.dialogueText += UpdateText;
        Actions.updateDialogueText += UpdateDialogue;
        Actions.updateUIText += UpdateText;

        ExplorationQuiz.UpdateQuestionText += UpdateText;

        BossQuiz.UpdateQuestionText += UpdateText;

        Actions.UpdateQuestionText += UpdateText;
    }

    private void OnDisable()
    {
        Actions.nameText -= UpdateText;
        Actions.dialogueText -= UpdateText;
        Actions.updateDialogueText -= UpdateDialogue;
        Actions.updateUIText -= UpdateText;

        ExplorationQuiz.UpdateQuestionText -= UpdateText;

        BossQuiz.UpdateQuestionText -= UpdateText;

        Actions.UpdateQuestionText -= UpdateText;
    }
}
