using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private bool forceDialogue;
    [SerializeField] private float distance = 1.5f;
    [SerializeField] private bool enableGizmo = false;

    //This function makes sure that the collider trigger is set to true.
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private bool DistanceFromPlayer()
    {
        float DistanceFromPlayer = Vector2.Distance(Actions.playerPos().position, transform.position);

        bool near = false;

        if (DistanceFromPlayer <= distance)
            near = true;
        return near;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (!enableGizmo) return;

        Gizmos.DrawWireSphere(transform.position, distance);
    }

    //What is an abstract method? It is a placeholder, any class that is
    //inherited from this class must implement that method.
    //Self note: Same vibes with interface.

    public abstract void Interact();

    private void OnTriggerStay2D(Collider2D other)
    {
        if (forceDialogue && DistanceFromPlayer())
        {
            Interact();
            forceDialogue = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //In order to enable and disable the interactable icon.
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInteraction>().OpenInteractableIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerInteraction>().CloseInteractableIcon();
    }
}
