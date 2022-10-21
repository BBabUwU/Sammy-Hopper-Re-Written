using UnityEngine;
using UnityEngine.UI;

public class Close_ExtraView : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject viewWindow;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Close_window);
    }

    private void Close_window()
    {
        viewWindow.SetActive(false);
    }

}
