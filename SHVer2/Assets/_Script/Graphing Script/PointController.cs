using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public int xValue;
    public int yValue;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlotPoint);
    }

    private void PlotPoint()
    {
        Debug.Log("X value: " + xValue);
        Debug.Log("Y value: " + yValue);
    }
}
