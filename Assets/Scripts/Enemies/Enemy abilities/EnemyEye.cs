using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    [Range(0, 360)]
    [SerializeField] private float _viewAngle = 180f; 
    public float viewRadius = 5f; 
    public float detectionTimer = 2f; 
    private float viewDirection = 0f;

    [SerializeField] private LayerMask _obstacleMask; 

    private PlayerSpawnLocations _player; 
    private Transform _transform; 
    private Transform _transformPlayer;
    private bool _playerIsDetected = false;

    private void Start()
    {
        _transform = transform; 
        _player = GameObject.Find("CheckPoints").GetComponent<PlayerSpawnLocations>(); 
        _transformPlayer = _player._transformPlayer.transform;
    }

    /// <summary>
    /// Метод для обнаружения игрока
    /// </summary>
    public bool DetectPlayer()
    {
        return _playerIsDetected;
    }

    private void Update()
    {
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

    private bool IsPlayerInFieldOfView()
    {
        Vector2 directionToPlayer = (_transformPlayer.position - _transform.position).normalized;
        float angleToPlayer = Vector2.Angle(DirFromAngle(viewDirection, true), directionToPlayer);

        return angleToPlayer <= _viewAngle / 2f; // Проверка, попадает ли игрок в угол обзора
    }

    private bool IsPlayerBehindObstacle()
    {
        Vector2 directionToPlayer = (_transformPlayer.position - _transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(_transform.position, _transformPlayer.position);

        // Луч для проверки наличия препятствий
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, directionToPlayer, distanceToPlayer, _obstacleMask);
        return hit.collider != null; 
    }

    /// <summary>
    /// Меняет направление конуса обзора
    /// </summary>
    public void ChangeDirectionView(bool movingRight)
    {
        viewDirection = movingRight ? 0 : 180f;
    }

    /// <summary>
    /// Получает направление угла
    /// </summary>
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

        Vector3 viewAngleA = DirFromAngle(viewDirection - _viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewDirection + _viewAngle / 2, false);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius); 
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius); 
    }
}

