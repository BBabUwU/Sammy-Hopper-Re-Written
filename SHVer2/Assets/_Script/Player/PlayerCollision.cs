using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        IgnoreCollisions();
    }

    private void IgnoreCollisions()
    {
        Physics2D.IgnoreLayerCollision(8, 14);
        Physics2D.IgnoreLayerCollision(8, 15);
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
}
