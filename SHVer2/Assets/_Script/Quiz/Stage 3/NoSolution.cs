using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoSolution : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(NoAnswer);
    }

    private void NoAnswer()
    {
        Actions.NoAnswer?.Invoke();
    }
}
