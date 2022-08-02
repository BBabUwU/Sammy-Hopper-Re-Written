using UnityEngine;
using UnityEngine.UI;
using FreeDraw;

public class SliderChange : MonoBehaviour
{
    public Slider slider;
    public DrawingSettings _drawingSettings;

    private void Update()
    {
        Debug.Log(slider.value);
        _drawingSettings.SetMarkerWidth(slider.value);
    }
}

