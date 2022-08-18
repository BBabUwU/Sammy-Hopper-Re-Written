using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IPickable pickableItem = other.GetComponent<IPickable>();

        if (pickableItem != null)
        {
            pickableItem.PickUp();
        }
    }

    //Interactable Script


}
