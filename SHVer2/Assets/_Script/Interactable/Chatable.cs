using UnityEngine;

[RequireComponent(typeof(InteractionDialogue))]
public class Chatable : Interactable
{
    private bool isTalking = false;
    private InteractionDialogue dialogue;

    private void Awake()
    {
        dialogue = GetComponent<InteractionDialogue>();
    }
    public override void Interact()
    {
        if (!isTalking)
        {
            isTalking = true;
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
    }
}
