using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed = 5f;
    [SerializeField]
    private Vector3 GroundCheckLocalPoint;
    [SerializeField]
    private float JumpSpeed = 50;

    private Rigidbody2D _rigidbody;

    CameraFollow _cameraFollow;

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
        _rigidbody = GetComponent<Rigidbody2D>();
        _cameraFollow = FindObjectOfType<CameraFollow>();
        _cameraFollow.FindAnObjectToFollow(); // сообщаем камере что объект игрока создан
    }

    public void Jump()
    {
        if(IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpSpeed);
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2 { 
            x = InputHorizontal * HorizontalSpeed,
            y = _rigidbody.velocity.y 
        };

        if (_rigidbody.velocity.x > 0.5f) // меняем позицию камеры в зависимости от направления движения
            _cameraFollow.ChangeOffsetX(10);
        else if (_rigidbody.velocity.x < -0.5f)
            _cameraFollow.ChangeOffsetX(-10);
    }

    private bool IsGrounded()
    {
        if(Physics2D.OverlapPoint(transform.position + GroundCheckLocalPoint) != null)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawSphere(transform.position + GroundCheckLocalPoint, 0.1f);
    }
}
