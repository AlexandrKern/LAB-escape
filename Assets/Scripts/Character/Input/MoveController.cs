using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed = 5f;

    private Rigidbody2D _rigidbody;

    public float InputHorizontal { get; set; } 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2 { x = InputHorizontal * HorizontalSpeed, y = 0f };
    }
}
