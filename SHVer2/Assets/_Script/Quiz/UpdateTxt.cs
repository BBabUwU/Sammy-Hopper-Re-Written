using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTxt : MonoBehaviour
{
    public UITextType textType;
    private TextMeshProUGUI theText;

    private void Awake()
    {
        theText = GetComponent<TextMeshProUGUI>();
    }

    private void SetText(string text, UITextType textType)
    {
        if (this.textType == textType)
        {
            this.theText.text = text;
        }
    }

    private void OnEnable()
    {
        Actions.updateText += SetText;
    }

    private void OnDisable()
    {
        Actions.updateText -= SetText;
    }
}
