using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extras_Item : MonoBehaviour, IPickable
{
    public void PickUp()
    {
        Actions.IncrementExtra?.Invoke();
        Destroy(gameObject);
    }
}
