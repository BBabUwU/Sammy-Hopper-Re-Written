using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isSet = false;
    public float uiFadeTime = 2f;
    public bool isInitial = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isSet)
            {
                isSet = true;
                Actions.SetCheckPoint?.Invoke(transform.position);

                if (!isInitial)
                {
                    UIManager.Instance.TurnOnUI(UIType.UpdateIndicator);
                    Actions.UpdateIndicator?.Invoke("Checkpoint Reached");
                    StartCoroutine(disableIndicator());
                    isInitial = true;
                }
            }
        }
    }

    IEnumerator disableIndicator()
    {
        yield return new WaitForSeconds(uiFadeTime);
        UIManager.Instance.TurnOffUI(UIType.UpdateIndicator);
    }
}
