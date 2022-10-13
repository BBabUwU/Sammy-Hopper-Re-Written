using UnityEngine;
using TMPro;

public class UIGetDialogueText : MonoBehaviour
{
    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private string GetDialogueText()
    {
        return textUI.text;
    }

    private void OnEnable()
    {
        Actions.getDialoguetUIText += GetDialogueText;
    }

    private void OnDisable()
    {
        Actions.getDialoguetUIText += GetDialogueText;
    }
}
