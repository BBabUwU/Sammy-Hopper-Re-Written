using UnityEngine;
using UnityEngine.UI;

public class SwitchOption : MonoBehaviour
{
    public Transform bookmarkSelect;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchSelector);
    }

    public void SwitchSelector()
    {
        bookmarkSelect.position = transform.position;
    }
}
