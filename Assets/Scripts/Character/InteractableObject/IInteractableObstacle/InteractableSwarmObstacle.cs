using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SwarmObstacle))]
public class InteractableSwarmObstacle : MonoBehaviour, IInteractableObstacle
{
    private SwarmObstacle _swarmObstacle;

    private void Awake()
    {
        _swarmObstacle = GetComponent<SwarmObstacle>();
    }

    public void Interact()
    {
        _swarmObstacle.Translate();
    }
}
