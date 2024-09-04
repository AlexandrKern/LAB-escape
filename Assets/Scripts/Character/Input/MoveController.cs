using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
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
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2 { x = InputHorizontal * HorizontalSpeed, y = 0f };
    }
}
