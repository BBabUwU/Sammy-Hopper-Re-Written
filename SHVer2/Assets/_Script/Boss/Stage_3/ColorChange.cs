using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        ChangeColor(0);
    }

    private void ChangeColor(int x)
    {
        if (x == 0) _renderer.color = new Color32(255, 255, 255, 255);
        else if (x == 1) _renderer.color = new Color32(255, 150, 150, 255);
        else if (x == 2) _renderer.color = new Color32(255, 120, 120, 255);
        else if (x == 3) _renderer.color = new Color32(255, 0, 0, 255);
    }

    private void OnEnable()
    {
        Actions.interactColor += ChangeColor;
    }

    private void OnDisable()
    {
        Actions.interactColor -= ChangeColor;
    }
}
