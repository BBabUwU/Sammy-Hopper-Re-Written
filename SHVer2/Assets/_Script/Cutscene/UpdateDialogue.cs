using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateDialogue : MonoBehaviour
{
    [SerializeField] private List<string> lines = new List<string>();
    [SerializeField] private float textSpeed = 0.03f;
    private int currentLineIndex = 0;
    private TextMeshProUGUI theText;

    private void Awake()
    {
        theText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        theText.text = "";
        StartCoroutine(TypeLine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[currentLineIndex].ToCharArray())
        {
            theText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
