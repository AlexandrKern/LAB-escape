using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private float _maxSoundRadius = 5f;

    [Range(0, 1)]
    [SerializeField] private float _soundVolume = 0.5f;


    private CircleCollider2D soundCollider;
    private MoveController moveController;

    private void Awake()
    {
        soundCollider = GetComponent<CircleCollider2D>();

        if (!soundCollider.isTrigger)
        {
            soundCollider.isTrigger = true;
        }

        moveController = GetComponentInParent<MoveController>();
    }

    private void Update()
    {
        if (moveController != null)
        {
            float currentSpeed = moveController.CurrentSpeed;
            _soundVolume = Mathf.Clamp01(currentSpeed / moveController.SoundSpeed);

            UpdateColliderSize();
        }
    }

    private void UpdateColliderSize()
    {
        float currentRadius = _soundVolume * _maxSoundRadius;
        soundCollider.radius = currentRadius;
    }
}













