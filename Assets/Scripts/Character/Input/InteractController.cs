using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField]
    private float InteractiveCircleRadius = 5f;

    [Space]
    [SerializeField]
    private bool DebugCircle;

    public void Interact()
    {
        var overlaped = Physics2D.OverlapCircleAll(transform.position, InteractiveCircleRadius);
        for(int i = 0; i < overlaped.Length; i++)
        {
            if(overlaped[i].gameObject
                .TryGetComponent<IInteractable>(out IInteractable interactable))
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
