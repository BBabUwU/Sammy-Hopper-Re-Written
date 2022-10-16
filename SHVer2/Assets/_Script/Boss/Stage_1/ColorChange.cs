using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer theRenderer;

    private void Awake()
    {
        theRenderer = GetComponent<SpriteRenderer>();
    }
}
