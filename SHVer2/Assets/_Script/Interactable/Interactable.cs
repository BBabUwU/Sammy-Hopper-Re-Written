using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{

    //This function makes sure that the collider trigger is set to true.
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    //What is an abstract method? It is a placeholder, any class that is
    //inherited from this class must implement that method.
    //Self note: Same vibes with interface.

    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D other)
    {
        //In order to enable and disable the interactable icon.
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerInteraction>().OpenInteractableIcon();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerInteraction>().CloseInteractableIcon();
    }
}
