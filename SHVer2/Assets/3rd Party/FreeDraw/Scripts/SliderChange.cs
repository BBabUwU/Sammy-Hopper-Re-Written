using UnityEngine;
using UnityEngine.UI;
using FreeDraw;

public class SliderChange : MonoBehaviour
{
    public Slider slider;
    public DrawingSettings _drawingSettings;

    private void Update()
    {
        _drawingSettings.SetMarkerWidth(slider.value);
    }
}

