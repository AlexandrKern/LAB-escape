using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    #region SoundEmitter
  
    [SerializeField] private float _maxSoundRadius = 5f;

    [Range(0, 1)]
    [SerializeField] 
    private float _soundVolume = 0.5f;

    private bool visualizeSound = true;

    private CircleCollider2D soundCollider;
    #endregion

    [SerializeField]
    private float HorizontalSpeed = 5f;

    private Rigidbody2D _rigidbody;

    private float _inputHorizontal = 0f;
    public float InputHorizontal
    {
        private get
        {
            return _inputHorizontal;
        }
        set
        {
            _inputHorizontal = value;

            //character turn
            if (value != 0f)
            {
                var oldScale = transform.localScale;
                transform.localScale = new Vector3(
                    value > 0 ? Mathf.Abs(oldScale.x): -Mathf.Abs(oldScale.x),
                    oldScale.y,  
                    oldScale.z);
            }
        }
    }

    private void Awake()
    {
        soundCollider = GetComponent<CircleCollider2D>();
        if (!soundCollider.isTrigger)
        {
            soundCollider.isTrigger = true;
        }
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2 { x = InputHorizontal * HorizontalSpeed, y = 0f };

        UpdateColliderSize();
        float speed = _rigidbody.velocity.magnitude;
        _soundVolume = Mathf.Clamp01(speed / HorizontalSpeed);

        
        
    }
    #region SoundEmitterMetods
    private void UpdateColliderSize()
    {
        float currentRadius = _soundVolume * _maxSoundRadius;

        if (soundCollider is CircleCollider2D circleCollider)
        {
            circleCollider.radius = currentRadius;
        }
    }

    // Визуализация зоны звука в редакторе
    private void OnDrawGizmos()
    {
        if (visualizeSound && soundCollider != null)
        {
            Gizmos.color = Color.yellow;
            float currentRadius = _soundVolume * _maxSoundRadius;

            if (soundCollider is CircleCollider2D)
            {
                Gizmos.DrawWireSphere(transform.position, currentRadius);
            }
        }
    }
    #endregion

}
