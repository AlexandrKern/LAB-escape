using System.Collections.Generic;
using UnityEngine;

public class MoveHunter : MonoBehaviour
{
    public float speed = 2f;
    private Transform _transform;
    private bool _movingRight = true;

    [HideInInspector] public Transform transformPlayer;
    private PlayerSpawnLocations _player;

    private Queue<Vector2> _playerPath = new Queue<Vector2>(); 
    public float recordInterval = 0.2f; 
    private float _recordTimer;

    private Vector2 _lastRecordedPosition; 
    public float minDistanceToRecord = 0.5f; 

    private void Start()
    {
        
        _player = GameObject.Find("CheckPoints").GetComponent<PlayerSpawnLocations>();
        transformPlayer = _player._transformPlayer.transform;
        _transform = transform;
        // ≈сли в очереди нет точек, добавл€ем стартовую позицию
        if (_playerPath.Count == 0)
        {
            _playerPath.Enqueue(transformPlayer.position);
            _lastRecordedPosition = transformPlayer.position; 
        }
    }
    /// <summary>
    /// ѕолучение рассто€ни€ до игрока
    /// </summary>
    /// <returns></returns>
    public float GetDistanceToPlayer() => Vector2.Distance(transform.position, transformPlayer.position);
    public void Move()
    {
        RecordPlayerPath();
        FollowPath();
        Debug.Log(" оличество созданых точек " + _playerPath.Count);
    }

    /// <summary>
    /// «аписывает путь игрока с заданным интервалом
    /// </summary>
    private void RecordPlayerPath()
    {
        _recordTimer += Time.deltaTime;
        if (_recordTimer >= recordInterval)
        {
            _recordTimer = 0f;
            Vector2 newPosition = transformPlayer.position;

            // ѕровер€ем, если нова€ точка достаточно отличаетс€ от последней записанной
            if (Vector2.Distance(_lastRecordedPosition, newPosition) > minDistanceToRecord)
            {
                _playerPath.Enqueue(newPosition); 
                _lastRecordedPosition = newPosition; 
            }
        }
    }

    /// <summary>
    /// ƒвигает врага по запомненному пути
    /// </summary>
    private void FollowPath()
    {
        if (_playerPath.Count > 0)
        {
            Vector2 target = _playerPath.Peek();
            MoveTo(target);

            if (Vector2.Distance(_transform.position, target) < 0.1f)
            {
                _playerPath.Dequeue(); 
            }
        }
    }

    public void MoveTo(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)_transform.position).normalized;
        _transform.position += (Vector3)direction * speed * Time.deltaTime;
        LookInDirection(direction);
    }

    private void LookInDirection(Vector2 direction)
    {
        if (direction.x > 0 && !_movingRight)
        {
            _movingRight = true;
            _transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0 && _movingRight)
        {
            _movingRight = false;
            _transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void ClearPath()
    {
        _playerPath.Clear();
    }
}


