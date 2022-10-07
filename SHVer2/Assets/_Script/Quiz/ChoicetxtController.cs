using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoicetxtController : MonoBehaviour
{
    public Choice choice;
    private TextMeshProUGUI theText;

    private void Awake()
    {
        theText = GetComponent<TextMeshProUGUI>();
    }

    private void SetText(Choice choice, string display)
    {
        if (this.choice == choice)
        {
            theText.text = display;
        }
    }

    private void OnEnable()
    {
        Actions.updateChoiceText += SetText;
    }

    private void OnDisable()
    {
        Actions.updateChoiceText -= SetText;
    }
}
