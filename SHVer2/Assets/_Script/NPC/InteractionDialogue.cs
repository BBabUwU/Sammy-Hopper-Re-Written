using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class InteractionDialogue : MonoBehaviour
{
    [Header("UI components")]
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private string npcName;

    [SerializeField] private List<string> lines = new List<string>();

    [SerializeField] private float textSpeed = 0.05f;

    [HideInInspector] public bool doneTalking;

    /// <Summary>
    /// Tells what line number the dialogue is at.
    /// Assigns the current dialogue.
    /// </summary>
    private int currentLineIndex;
    private List<string> currentLines;

    [Header("Quest complete lines")]
    [Header("Only set if NPC has quest")]
    [SerializeField] private bool hasQuest;
    private QuestGiver questGiver;
    [SerializeField] private List<string> questCompleteLines = new List<string>();

    private void Awake()
    {
        if (hasQuest)
        {
            questGiver = GetComponent<QuestGiver>();
        }
    }

    private void SetLines()
    {
        currentLines = lines;

        if (questGiver != null)
        {
            if (questGiver.quest.completed)
            {
                currentLines = questCompleteLines;
            }
        }
    }

    public void StartDialogue()
    {
        GameManager.Instance.UpdateGameState(GameState.NPCInteraction);
        SetLines();
        doneTalking = false;
        npcNameText.text = npcName;
        dialogueText.text = string.Empty;
        currentLineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SkipText()
    {
        if (dialogueText.text != currentLines[currentLineIndex])
        {
            StopAllCoroutines();
            dialogueText.text = currentLines[currentLineIndex];
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
            doneTalking = true;
            GameManager.Instance.UpdateGameState(GameState.Exploration);
        }
    }

    public bool StillHasLines()
    {
        return currentLineIndex < currentLines.Count - 1;
    }

}
