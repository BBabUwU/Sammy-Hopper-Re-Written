using UnityEngine;
using System;

public class PlayerNotepad : MonoBehaviour
{
    private PlayerInput playerInput;

    //Events
    public static Action OnNotepadEnabled;
    public static Action OnNotepadDisabled;

    [SerializeField] private bool isUsing;
    [SerializeField] private GameObject notepad;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    public void SwitchToNotepad()
    {
        if (playerInput.NotepadButtonPressed() && isUsing)
        {
            isUsing = false;
            notepad.SetActive(false);
            OnNotepadDisabled?.Invoke();
        }

        else if (playerInput.NotepadButtonPressed() && !isUsing)
        {
            isUsing = true;
            notepad.SetActive(true);
            OnNotepadEnabled?.Invoke();
        }
    }
}