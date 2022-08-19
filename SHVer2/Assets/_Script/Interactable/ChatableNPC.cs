using UnityEngine;

[RequireComponent(typeof(InteractionDialogue))]
[RequireComponent(typeof(NonPlayableCharacter))]
public class ChatableNPC : Interactable
{
    private bool isTalking = false;
    private InteractionDialogue dialogue;
    private NonPlayableCharacter npcScript;

    private void Awake()
    {
        dialogue = GetComponent<InteractionDialogue>();
        npcScript = GetComponent<NonPlayableCharacter>();
    }
    public override void Interact()
    {
        if (!isTalking)
        {
            isTalking = true;
            npcScript.SwitchDirection();
            npcScript.LookAtDirection();
            dialogue.StartDialogue();
        }

        else if (isTalking && !dialogue.doneTalking)
        {
            dialogue.SkipText();
        }

        if (isTalking && dialogue.doneTalking)
        {
            isTalking = false;
        }

        if (!isTalking && dialogue.doneTalking)
        {
            StartCoroutine(npcScript.LookBackAtDefaultDirection());
        }
    }
}
