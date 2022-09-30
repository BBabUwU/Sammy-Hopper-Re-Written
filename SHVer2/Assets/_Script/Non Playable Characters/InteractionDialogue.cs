using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class InteractionDialogue : MonoBehaviour
{
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
    [SerializeField] private List<string> questCompleteLines;

    //Events 
    public static event Action<UITextType, string> nameText;
    public static event Action<UITextType, string> dialogueText;
    public static event Action<char> updateDialogueText;
    public static event Func<UITextType, string> currentUIText;
    public static event Action<UITextType, string> updateUIText;

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
        nameText?.Invoke(UITextType.NameText, npcName);
        dialogueText?.Invoke(UITextType.DialogueText, string.Empty);
        currentLineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLines[currentLineIndex].ToCharArray())
        {
            updateDialogueText?.Invoke(c);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SkipText()
    {
        if (currentUIText(UITextType.DialogueText) != currentLines[currentLineIndex])
        {
            StopAllCoroutines();
            updateUIText?.Invoke(UITextType.DialogueText, currentLines[currentLineIndex]);
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
            updateUIText?.Invoke(UITextType.DialogueText, string.Empty);
            currentLineIndex++;
            StartCoroutine(TypeLine());
        }
        else
        {
            doneTalking = true;
            UIManager.Instance.TurnOffUI(UIType.DialogueUI);
            GameManager.Instance.UpdateGameState(GameState.Exploration);
        }
    }

    public bool StillHasLines()
    {
        return currentLineIndex < currentLines.Count - 1;
    }
}
