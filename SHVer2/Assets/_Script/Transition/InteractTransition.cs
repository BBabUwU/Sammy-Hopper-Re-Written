using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTransition : Interactable
{
    public override void Interact()
    {
        Actions.setMovement?.Invoke(false);
        Actions.setWeapon?.Invoke(false);
        Actions.setNotepad?.Invoke(false);
        Actions.setInventory?.Invoke(false);

        CanvasManager.Instance.SwitchCanvas(CanvasType.BossTransition);
    }
}
