using UnityEngine;
using UnityEngine.UI;

public class LeaveQuiz : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ReturnMessage);
    }

    private void ReturnMessage()
    {
        Actions.leaveQuiz?.Invoke();
    }
}
