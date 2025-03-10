using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed = 5f;
    [SerializeField]
    private Vector3 GroundCheckLocalPoint;
    [SerializeField]
    private float groundCheckAreaRadius = 0.1f;
    [SerializeField]
    private float JumpSpeed = 50;
    [SerializeField]
    private LayerMask groundLayerMask;

    [Space]
    [Header("Attack jump properties")]
    [SerializeField]
    private float attackJumpHorizontalSpeed;
    [SerializeField]
    private float attackJumpLength;
    [SerializeField]
    private float attackJumpHaight;

    private HintController _hintController;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    bool isFacingRight = true;
    #region SoundEmitter
    public float CurrentSpeed => _rigidbody.velocity.magnitude;
    public float SoundSpeed => HorizontalSpeed;
    #endregion
    public Vector3 CurrentDirection { get; private set; }

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
                isFacingRight = value > 0;

                transform.localScale = new Vector3(
                    isFacingRight ? Mathf.Abs(oldScale.x) : -Mathf.Abs(oldScale.x),
                    oldScale.y,
                    oldScale.z);

                //_hintController.SetHintTextRotation(isFacingRight ? 0 : 180);
            }
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _hintController = GetComponent<HintController>();
    }

    public async UniTask JumpForward()
    {
        if (IsGrounded())
        {
            float x = 0f;
            float coef = -4f * attackJumpHaight / (attackJumpLength * attackJumpLength);
            float horizontalSpeed = 
                isFacingRight ? attackJumpHorizontalSpeed : -attackJumpHorizontalSpeed;
            while(x < attackJumpLength)
            {
                x += attackJumpHorizontalSpeed * Time.fixedDeltaTime;
                float verticalSpeed = coef * (2f * x - attackJumpLength) * attackJumpHorizontalSpeed;
                Vector2 deltaPosition = 
                    new Vector2(horizontalSpeed, verticalSpeed) * Time.fixedDeltaTime;
                _rigidbody.MovePosition(_rigidbody.position + deltaPosition);
                await UniTask.WaitForFixedUpdate();
            }
        }
    }

    public void Jump()
    {
        if(IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpSpeed);
        }
    }

    readonly LayerMask PLATFORM_EFFECTOR_LAYER = 1 << 3; //3 - PlatformEffector layer number
    const double FALL_THROUGH_DELAY = 0.2;
    public async UniTask FallThroughPlatformEffector()
    {
        _collider.excludeLayers |= PLATFORM_EFFECTOR_LAYER;
        await UniTask.Delay(TimeSpan.FromSeconds(FALL_THROUGH_DELAY), ignoreTimeScale: false);
        _collider.excludeLayers &= ~PLATFORM_EFFECTOR_LAYER;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2 { 
            x = InputHorizontal * HorizontalSpeed,
            y = _rigidbody.velocity.y 
        };
        CurrentDirection = new Vector3(_inputHorizontal, 0, 0).normalized;
    }

    private bool IsGrounded()
    {
        var overlaped = 
            Physics2D.OverlapCircle(transform.position + GroundCheckLocalPoint, 
            groundCheckAreaRadius, groundLayerMask);
        if(overlaped != null)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawSphere(transform.position + GroundCheckLocalPoint, groundCheckAreaRadius);
    }
}
