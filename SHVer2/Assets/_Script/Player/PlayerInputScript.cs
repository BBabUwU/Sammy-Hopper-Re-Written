using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    internal float HorizontalAxis()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    internal bool JumpIsPressed()
    {
        return Input.GetButtonDown("Jump");
    }

    internal bool JumpIsReleased()
    {
        return Input.GetButtonUp("Jump");
    }

    internal bool WeaponFirePressed()
    {
        return Input.GetButton("Fire1");
    }

    internal bool NotepadButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.N);
    }

    public bool InteractionButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
