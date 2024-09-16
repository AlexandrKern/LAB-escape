using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed = 5f;

    private Rigidbody2D _rigidbody;

    private float _inputHorizontal = 0f;

    CameraFollow _cameraFollow;
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

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2 { x = InputHorizontal * HorizontalSpeed, y = 0f };

        if (_rigidbody.velocity.x > 0.5f) // меняем позицию камеры в зависимости от направления движения
            _cameraFollow.ChangeOffsetX(10);
        else if (_rigidbody.velocity.x < -0.5f)
            _cameraFollow.ChangeOffsetX(-10);
    }
}
