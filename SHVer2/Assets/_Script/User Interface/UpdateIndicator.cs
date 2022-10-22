using UnityEngine;
using TMPro;

public class UpdateIndicator : MonoBehaviour
{
    private TextMeshProUGUI text;
    public float fadeTime = 2f;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string x)
    {
        text.text = x;

    }

    private void OnEnable()
    {
        Actions.UpdateIndicator += UpdateText;
    }

    private void OnDisable()
    {
        Actions.UpdateIndicator -= UpdateText;
    }
}
