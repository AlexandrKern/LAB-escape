using UnityEngine;

public class MechanicalDoor : MonoBehaviour, IInteractableDestroyable
{
    public void Interact()
    {
        //TODO: destroy animation here
        Destroy(gameObject);
    }
}
