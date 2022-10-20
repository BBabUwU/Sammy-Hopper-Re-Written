using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerWeapon playerWeapon;
    private bool isUsing;
    private bool canUse;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerWeapon = GetComponent<PlayerWeapon>();
    }

    public void OpenInventory()
    {
        if (playerInput.InventoryButton() && !isUsing && canUse)
        {
            CanvasManager.Instance.SwitchCanvas(CanvasType.Inventory);
            playerWeapon.allowedToFire = false;
            isUsing = true;
            Actions.setNotepad?.Invoke(false);
        }
        else if (playerInput.InventoryButton() && isUsing)
        {
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
            playerWeapon.allowedToFire = true;
            isUsing = false;
            Actions.setNotepad?.Invoke(true);
        }
    }

    private void SetInventory(bool x)
    {
        canUse = x;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Exploration)
        {
            canUse = true;
        }

        else
        {
            canUse = false;
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        Actions.setInventory += SetInventory;
        Actions.setAllControls += SetInventory;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        Actions.setInventory -= SetInventory;
        Actions.setAllControls -= SetInventory;
    }
}
