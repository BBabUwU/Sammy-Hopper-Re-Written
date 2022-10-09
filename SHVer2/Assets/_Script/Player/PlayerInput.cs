using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalAxis()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool JumpIsPressed()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool JumpIsReleased()
    {
        return Input.GetButtonUp("Jump");
    }

    public bool WeaponFirePressed()
    {
        return Input.GetButtonDown("Fire1");
    }

    public bool NotepadButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.N);
    }

    public bool InteractionButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    public bool InventoryButton()
    {
        return Input.GetKeyDown(KeyCode.I);
    }

    public bool ParryButton()
    {
        return Input.GetMouseButtonDown(1);
    }
}
