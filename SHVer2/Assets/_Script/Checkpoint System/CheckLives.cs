using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLives : MonoBehaviour
{
    private void OnEnable()
    {
        Actions.Check_Lives?.Invoke();
    }
}
