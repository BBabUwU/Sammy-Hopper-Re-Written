using UnityEngine;
using UnityEngine.UI;
using FreeDraw;

public class NotepadControls : MonoBehaviour
{
    private Drawable drawable;
    private DrawingSettings drawSettings;

    public Button blackPenButton;
    public Button redPenButton;
    public Button eraserButton;
    public Button clearButton;


    private void Awake()
    {
        drawable = GetComponent<Drawable>();
        drawSettings = GetComponent<DrawingSettings>();
        blackPenButton.onClick.AddListener(BlackPen);
        redPenButton.onClick.AddListener(RedPen);
        eraserButton.onClick.AddListener(Eraser);
        clearButton.onClick.AddListener(Clear);
    }

    public void BlackPen()
    {
        drawSettings.SetMarkerWidth(3f);
        drawSettings.SetMarkerBlack();
    }

    public void RedPen()
    {
        drawSettings.SetMarkerWidth(3f);
        drawSettings.SetMarkerRed();
    }

    public void Eraser()
    {
        drawSettings.SetMarkerWidth(10f);
        drawSettings.SetEraser();
    }

    public void Clear()
    {
        drawable.ResetCanvas();
    }
}
