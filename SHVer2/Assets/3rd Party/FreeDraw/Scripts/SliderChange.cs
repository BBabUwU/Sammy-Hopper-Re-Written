using UnityEngine;
using UnityEngine.UI;
using FreeDraw;

public class SliderChange : MonoBehaviour
{
    public Slider _slider;
    public DrawingSettings _ds;

    private void Update()
    {
        Debug.Log(_slider.value);
        _ds.SetMarkerWidth(_slider.value);
    }
}

