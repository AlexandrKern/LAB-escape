using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private LaserMove laserMove;
     public float speed = 2f;
     public float chaseSpeed = 5f;
     public float patrolDistance = 10f;

    [Header("Timing Settings")]
    public float searchTime = 10;
    public float idleTimer = 3;

    private Rigidbody2D _rb;
    private Transform _transform;
    private Vector2 _startingPosition;
    private float _startSpeed;
    private bool _movingRight = true;
    private bool _isReturnPatrol;
    private bool _reachedEndOfPatrol;

    [HideInInspector] public Transform transformPlayer;
    [HideInInspector] public bool camePlaceOfSearch;

    private PlayerSpawnLocations _player;

    private void Start()
    {
        _startSpeed = speed;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("CheckPoints").GetComponent<PlayerSpawnLocations>();
        transformPlayer = _player._transformPlayer.transform;
        _transform = transform;
        _startingPosition = _transform.position;
    }

    /// <summary>
    ///  Получение расстояния до игрока
    /// </summary>
    /// <returns></returns>
    public float GetDistanceToPlayer() => Vector2.Distance(transform.position, transformPlayer.position);


    public bool Patrol(EnemyEye eye)
    {
        HandlePatrolEnd(eye);
        MoveEnemy();
        return _reachedEndOfPatrol;
    }

    /// <summary>
    /// Возврашение к патрулированию
    /// </summary>
    /// <param name="eye"></param>
    private void HandlePatrolEnd(EnemyEye eye)
    {
        if (_reachedEndOfPatrol)
        {
            _reachedEndOfPatrol = false;
            ChangeDirection(eye);
        }

        if (_isReturnPatrol)
        {
            _movingRight = !_movingRight;
            _isReturnPatrol = false;
            ChangeDirection(eye);
        }

        CheckPatrolBounds();
    }

    private void MoveEnemy()
    {
        Vector2 force = _movingRight ? Vector2.right : Vector2.left;
        _rb.AddForce(force * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        LookInDirection(force);
    }


    /// <summary>
    /// Проверяет достижение границ патрулирования
    /// </summary>
    private void CheckPatrolBounds()
    {
        float offsetFromStart = transform.position.x - _startingPosition.x;

        if ((_movingRight && offsetFromStart >= patrolDistance) || (!_movingRight && offsetFromStart <= -patrolDistance))
        {
            _rb.velocity = Vector2.zero;
            _reachedEndOfPatrol = true;
        }
    }

    /// <summary>
    /// Двигается к цели
    /// </summary>
    /// <param name="target"></param>
    public void MoveTo(Vector2 target, EnemyEye eye)
    {
        _isReturnPatrol = true;

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        float distanceToTarget = Vector2.Distance(transform.position, target);

        if (distanceToTarget > 3f)
        {
            MoveTowardsTarget(direction);
        }
        else
        {
            camePlaceOfSearch = true;
            _rb.velocity = Vector2.zero;
        }

        LookInDirection(direction);
        LimitSpeed();
    }

    private void MoveTowardsTarget(Vector2 direction)
    {
        direction.y = 0;
        _rb.AddForce(direction * chaseSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        if (direction.x > 0 != _movingRight)
        {
            _movingRight = direction.x > 0;
        }
    }

    /// <summary>
    /// Ограничивает скорост движения
    /// </summary>
    private void LimitSpeed()
    {
        if (Mathf.Abs(_rb.velocity.x) > speed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * speed, _rb.velocity.y);
        }
    }


    /// <summary>
    /// Поворачивает врага в нужном направлении
    /// </summary>
    /// <param name="direction"></param>
    private void LookInDirection(Vector2 direction)
    {
        if (direction.x != 0)
        {
            float rotationY = direction.x > 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }

    /// <summary>
    /// Изменяет направление движения
    /// </summary>
    /// <param name="eye"></param>
    private void ChangeDirection(EnemyEye eye)
    {
        _movingRight = !_movingRight;
        _rb.velocity = Vector2.zero;

        laserMove.SetLaserPos(_movingRight);

        eye.ChangeDirectionView(_movingRight);
    }

    /// <summary>
    /// Остонавливает врага
    /// </summary>
    public void StopMovement()
    {
        speed = 0;
    }

    /// <summary>
    /// Востонавливает скорость
    /// </summary>
    public void RestoreDefaultSpeed()
    {
        speed = _startSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * patrolDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * patrolDistance);
    }
}
