using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    //These are default variables
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string npcName;
    [SerializeField] private List<string> lines = new List<string>();
    [SerializeField] private float textSpeed = 0.05f;
    private int currentLineIndex;
    private List<string> currentLines;

    //Used for flipping the direction of the NPC.
    private InteractNPC _flipNPC;

    //--------------------------------------------------------------------------------------------
    //If NPC has a side quest
    [SerializeField] private bool hasQuest = false;
    [Header("Only set if NPC has a quest")]
    [SerializeField] private List<string> questCompleteLines = new List<string>();
    QuestManager questManager;

    private void Awake()
    {
        _flipNPC = GetComponent<InteractNPC>();
        questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    private void SetLines()
    {
        currentLines = lines;

        if (questManager.QuestIsCompleted(gameObject.tag))
        {
            currentLines = questCompleteLines;
        }
    }

    public void StartDialogue()
    {
        SetLines();
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
            GameManager.Instance.UpdateGameState(GameState.Exploration);
            StartCoroutine(_flipNPC.LookBackAtDefaultDirection());
        }
    }

    private bool StillHasLines()
    {
        return currentLineIndex < currentLines.Count - 1;
    }
}
