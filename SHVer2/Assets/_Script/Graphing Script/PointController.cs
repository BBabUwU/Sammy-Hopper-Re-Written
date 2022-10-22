using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public int xValue;
    public int yValue;
    private Button button;
    [HideInInspector] public Image image;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(PlotPoint);
    }
    private void Start()
    {
        image.color = Color.clear;
    }

    private void PlotPoint()
    {
        if (Actions.CurrentAxis() == CurrentAxis.theX)
        {
            Actions.SetAxis?.Invoke(xValue, yValue, this.gameObject);
        }

        else if (Actions.CurrentAxis() == CurrentAxis.theY)
        {
            Actions.SetAxis?.Invoke(xValue, yValue, this.gameObject);
        }
    }
}
