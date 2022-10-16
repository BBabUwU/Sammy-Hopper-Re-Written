using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class InteractionDialogue : MonoBehaviour
{
    [SerializeField] private List<string> whoIsTalking = new List<string>();
    [SerializeField] private List<string> lines = new List<string>();
    [SerializeField] private float textSpeed = 0.05f;
    [HideInInspector] public bool doneTalking;
    private BoxCollider2D col;
    private SpriteRenderer sprite;

    /// <Summary>
    /// Tells what line number the dialogue is at.
    /// Assigns the current dialogue.
    /// </summary>
    private int currentLineIndex;
    private List<string> currentName;
    private List<string> currentLines;

    [Header("Quest complete lines")]
    [Header("Only set if NPC has quest")]
    [SerializeField] private bool hasQuest;
    [SerializeField] private bool hasInitialBlockArea = false;
    [SerializeField] private GameObject initialBarrier;
    private QuestGiver questGiver;
    [SerializeField] private List<string> completeWhoIsTalking;
    [SerializeField] private List<string> questCompleteLines;

    private bool HasAdded = false;

    private void Awake()
    {

        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (hasQuest)
        {
            questGiver = GetComponent<QuestGiver>();
        }
    }

    private void SetLines()
    {
        currentName = whoIsTalking;
        currentLines = lines;

        SetQuestGiver();
    }

    private void SetQuestGiver()
    {
        if (questGiver != null)
        {

            questGiver.CheckIfFinish(questGiver.quest.questID);

            if (questGiver.quest.completed)
            {
                currentName = completeWhoIsTalking;
                currentLines = questCompleteLines;
            }
        }
    }

    public void StartDialogue()
    {
        GameManager.Instance.UpdateGameState(GameState.NPCInteraction);
        SetLines();
        doneTalking = false;
        Actions.dialogueText?.Invoke(UITextType.DialogueText, string.Empty);
        currentLineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        Actions.nameText?.Invoke(UITextType.NameText, currentName[currentLineIndex]);

        foreach (char c in currentLines[currentLineIndex].ToCharArray())
        {
            Actions.updateDialogueText?.Invoke(c);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SkipText()
    {
        if (Actions.getDialoguetUIText() != currentLines[currentLineIndex])
        {
            StopAllCoroutines();
            Actions.updateUIText?.Invoke(UITextType.DialogueText, currentLines[currentLineIndex]);
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
            Actions.updateUIText?.Invoke(UITextType.DialogueText, string.Empty);
            currentLineIndex++;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (hasQuest && !HasAdded)
            {
                Actions.addToList?.Invoke(questGiver);
                HasAdded = true;
            }

            if (hasQuest)
            {
                questGiver.quest.Evaluate();
            }

            if (hasInitialBlockArea)
            {
                Destroy(initialBarrier);
                hasInitialBlockArea = false;
            }

            doneTalking = true;
            UIManager.Instance.TurnOffUI(UIType.DialogueUI);
            GameManager.Instance.UpdateGameState(GameState.Exploration);

            if (questGiver != null && questGiver.quest.completed)
            {
                sprite.enabled = false;
                col.enabled = false;
            }
        }
    }

    public bool StillHasLines()
    {
        return currentLineIndex < currentLines.Count - 1;
    }
}
