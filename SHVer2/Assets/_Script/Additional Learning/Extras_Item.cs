using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extras_Item : MonoBehaviour, IPickable
{
    public float uiFadeTime = 2f;
    private CircleCollider2D col;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void PickUp()
    {
        col.enabled = false;
        _renderer.enabled = false;
        Actions.IncrementExtra?.Invoke();
        UIManager.Instance.TurnOnUI(UIType.UpdateIndicator);
        Actions.UpdateIndicator?.Invoke("Extra Information Added");
        StartCoroutine(disableIndicator());
    }

    IEnumerator disableIndicator()
    {
        yield return new WaitForSeconds(uiFadeTime);
        UIManager.Instance.TurnOffUI(UIType.UpdateIndicator);
        Destroy(gameObject);
    }
}
