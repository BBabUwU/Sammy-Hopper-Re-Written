using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

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
    private QuestGiver questGiver;
    [SerializeField] private List<string> completeWhoIsTalking;
    [SerializeField] private List<string> questCompleteLines;

    //Events 
    public static event Action<UITextType, string> nameText;
    public static event Action<UITextType, string> dialogueText;
    public static event Action<char> updateDialogueText;
    public static event Func<UITextType, string> currentUIText;
    public TextMeshProUGUI currentTextUI;
    public static event Action<UITextType, string> updateUIText;

    //Add quest
    public static event Action<QuestGiver> addToList;
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
        dialogueText?.Invoke(UITextType.DialogueText, string.Empty);
        currentLineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        nameText?.Invoke(UITextType.NameText, currentName[currentLineIndex]);

        foreach (char c in currentLines[currentLineIndex].ToCharArray())
        {
            updateDialogueText?.Invoke(c);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SkipText()
    {
        //currentUIText(UITextType.DialogueText)
        if (currentTextUI.text != currentLines[currentLineIndex])
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
            if (hasQuest && !HasAdded)
            {
                addToList?.Invoke(questGiver);
                HasAdded = true;
            }

            if (hasQuest)
            {
                questGiver.quest.Evaluate();
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
