using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTopic : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Display_Topic(string currentTopic)
    {
        text.text = currentTopic;
    }

    private void OnEnable()
    {
        Actions.UpdateTopic += Display_Topic;
    }

    private void OnDisable()
    {
        Actions.UpdateTopic -= Display_Topic;
    }
}
