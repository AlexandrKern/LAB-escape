using UnityEngine;

public class GlassDoor : MonoBehaviour, IInteractableDestroyable
{
    public void Interact()
    {
        //TODO: destroy animation here
        Destroy(gameObject);
    }
}
