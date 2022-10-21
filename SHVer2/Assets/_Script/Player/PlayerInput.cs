using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Make it visible toggalable
    private void Start() { }
    public float HorizontalAxis()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool JumpIsPressed()
    {
        return Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.J);
    }

    public bool JumpIsReleased()
    {
        return Input.GetButtonUp("Jump");
    }

    public bool WeaponFirePressed()
    {
        return Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.K);
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
        return Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L);
    }

    public bool PauseButton()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
