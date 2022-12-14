using UnityEngine;

[RequireComponent(typeof(InteractionDialogue))]
public class ChatableNPC : Interactable
{
    [SerializeField] private bool isPerson;
    private bool isTalking = false;
    private InteractionDialogue dialogue;
    private NonPlayableCharacter npcScript;

    private void Awake()
    {
        dialogue = GetComponent<InteractionDialogue>();
        if (isPerson) npcScript = GetComponent<NonPlayableCharacter>();
    }
    public override void Interact()
    {
        if (!isTalking)
        {
            isTalking = true;

            if (isPerson)
            {
                StopAllCoroutines();
                npcScript.SwitchDirection();
                npcScript.LookAtDirection();
            }

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
            if (isPerson) StartCoroutine(npcScript.LookBackAtDefaultDirection());
        }
    }
}
