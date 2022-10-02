using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool isUsing;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OpenInventory()
    {
        if (playerInput.InventoryButton() && !isUsing)
        {
            CanvasManager.Instance.SwitchCanvas(CanvasType.Inventory);
            isUsing = true;
        }
        else if (playerInput.InventoryButton() && isUsing)
        {
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
            isUsing = false;
        }
    }
}
