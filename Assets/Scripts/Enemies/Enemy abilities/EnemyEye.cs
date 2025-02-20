using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    [Range(0, 360)]
    [SerializeField] private float _viewAngle = 180f; 
    public float viewRadius = 5f; 
    public float detectionTimer = 2f; 
    private float viewDirection = 0f;

    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField, Range(0, 360)] private float _rotationAngle = 0f;

    private PlayerSpawnLocations _player; 
    private Transform _transform; 
    private Transform _transformPlayer;
    private bool _playerIsDetected = false;

    [Header("Auto Rotation Settings")]
    [SerializeField] private bool _canRotate = false; 
    [SerializeField] private float _rotationRange = 45f; // Максимальный угол вращения в обе стороны
    [SerializeField] private float _rotationSpeed = 1f; // Скорость вращения

    private float _startRotationAngle; // Базовый угол
    private float _rotationTime = 0f;

    private void Start()
    {
        _transform = transform;
        _player = GameObject.Find("CheckPoints").GetComponent<PlayerSpawnLocations>();
        _transformPlayer = _player._transformPlayer.transform;
        _startRotationAngle = _rotationAngle;
    }

    private void Update()
    {
        viewDirection = _rotationAngle;

        if (_canRotate)
        {
            RotateView();
        }

        float sqrDistanceToPlayer = Vector2.SqrMagnitude(_transform.position - _transformPlayer.position);

        if (sqrDistanceToPlayer <= viewRadius * viewRadius && IsPlayerInFieldOfView() && !IsPlayerBehindObstacle())
        {
            if (!_playerIsDetected)
            {
                _playerIsDetected = true;
                Character.Instance.OnDetection(this);
            }
        }
        else
        {
            if (_playerIsDetected)
            {
                _playerIsDetected = false;
                Character.Instance.OnMiss(this);
            }
        }
    }

    /// <summary>
    /// Метод для обнаружения игрока
    /// </summary>
    public bool DetectPlayer()
    {
        return _playerIsDetected;
    }

    /// <summary>
    /// Возвращает крайнюю точку центра конуса обзора
    /// </summary>
    public Vector3 GetViewCenterPoint()
    {
        Vector3 direction = DirFromAngle(_rotationAngle, false).normalized;
        return _transform.position + direction * viewRadius;
    }

    /// <summary>
    /// Возвращает ширину конуса обзора на максимальном расстоянии
    /// </summary>
    public float GetViewWidth()
    {
        return 2 * viewRadius * Mathf.Tan(_viewAngle * 0.5f * Mathf.Deg2Rad);
    }

    private void RotateView()
    {
        _rotationTime += Time.deltaTime * _rotationSpeed;
        _rotationAngle = _startRotationAngle + Mathf.Sin(_rotationTime) * _rotationRange;
    }

    /// <summary>
    /// Подстраивает центр конуса обзора под указанную точку
    /// </summary>
    public void AdjustViewToPoint(Vector3 targetPosition)
    {
        Vector2 directionToTarget = (targetPosition - _transform.position).normalized;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        _rotationAngle = angle;
    }

    private bool IsPlayerInFieldOfView()
    {
        Vector2 directionToPlayer = (_transformPlayer.position - _transform.position).normalized;
        float angleToPlayer = Vector2.Angle(DirFromAngle(viewDirection, true), directionToPlayer);
        return angleToPlayer <= _viewAngle / 2f;
    }

    private bool IsPlayerBehindObstacle()
    {
        Vector2 directionToPlayer = (_transformPlayer.position - _transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(_transform.position, _transformPlayer.position);
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, directionToPlayer, distanceToPlayer, _obstacleMask);
        return hit.collider != null;
    }

    public void ChangeDirectionView(bool movingRight)
    {
        _rotationAngle = movingRight ? 0 : 180f;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(_rotationAngle - _viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(_rotationAngle + _viewAngle / 2, false);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }
    public void StartAutoRotation()
    {
        _canRotate = true;
    }
    public void StopAutoRotation()
    {
        _canRotate = false;
    }
}




