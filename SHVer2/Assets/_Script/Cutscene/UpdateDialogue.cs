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
    [SerializeField] private bool enableSound;
    [SerializeField] private bool isDavis;


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
            if (enableSound)
            {
                if (isDavis) AudioManager.Instance.Play("voice_deep");
                else AudioManager.Instance.Play("voice_light");
            }

            theText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
