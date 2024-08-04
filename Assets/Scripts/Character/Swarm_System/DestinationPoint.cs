using UnityEngine;

public class DestinationPoint
{
    private Transform _transform;
    private bool _isFree;

    public DestinationPoint(Transform transform, bool isFree)
    {
        Transform = transform;
        IsFree = isFree;
    }

    public Transform Transform { get => _transform; private set => _transform = value; }
    public bool IsFree { get => _isFree; set => _isFree = value; }
}
