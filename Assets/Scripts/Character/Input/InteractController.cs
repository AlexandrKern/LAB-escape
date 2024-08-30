using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField]
    private float InteractiveCircleRadius = 5f;

    [Space]
    [SerializeField]
    private bool DebugCircle;

    public void Interact<InteractableInterface>() where InteractableInterface : IInteractable
    {
        var overlaped = Physics2D.OverlapCircleAll(transform.position, InteractiveCircleRadius);
        for(int i = 0; i < overlaped.Length; i++)
        {
            var interactable = overlaped[i].gameObject.GetComponentInParent<InteractableInterface>();
            if(interactable != null)
            {
                interactable.Interact();
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(DebugCircle)
        {
            Gizmos.DrawWireSphere(transform.position, InteractiveCircleRadius);
        }
    }
}
