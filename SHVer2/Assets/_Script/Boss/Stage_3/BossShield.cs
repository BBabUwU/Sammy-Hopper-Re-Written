using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer _renderer;
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(20, 21);
    }

    private void SwitchShield(bool x)
    {
        col.enabled = x;
        _renderer.enabled = x;
    }

    private void OnEnable()
    {
        Actions.switchShield += SwitchShield;
    }

    private void OnDisable()
    {
        Actions.switchShield -= SwitchShield;
    }
}
