using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string npcName;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed = 0.05f;
    private int currentLineIndex;

    public void StartDialogue()
    {
        npcNameText.text = npcName;
        dialogueText.text = string.Empty;
        currentLineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SkipText()
    {
        if (dialogueText.text != lines[currentLineIndex])
        {
            StopAllCoroutines();
            dialogueText.text = lines[currentLineIndex];
        }
        else
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        if (StillHasLines())
        {
            dialogueText.text = string.Empty;
            currentLineIndex++;
            StartCoroutine(TypeLine());
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Exploration);
        }
    }

    private bool StillHasLines()
    {
        return currentLineIndex < lines.Length - 1;
    }
}
