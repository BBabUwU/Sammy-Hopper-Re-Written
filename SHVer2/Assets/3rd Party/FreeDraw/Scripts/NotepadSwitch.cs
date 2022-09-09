using UnityEngine;

public class NotepadSwitch : MonoBehaviour
{
    private void SwitchOn()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void SwitchOff()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerNotepad.TurnOnNotepad += SwitchOn;
        PlayerNotepad.TurnOffNotepad += SwitchOff;
    }

    private void OnDisable()
    {
        PlayerNotepad.TurnOnNotepad -= SwitchOn;
        PlayerNotepad.TurnOffNotepad -= SwitchOff;
    }
}
