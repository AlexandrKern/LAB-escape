using UnityEngine;

public class CameraLaserMove : MonoBehaviour
{
    public Laser laser;
    [SerializeField] private Transform endLaser;
    private PlayerSpawnLocations _player;
    [HideInInspector] public Transform TransformPlayer;

    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 _targetPosition;
    private bool _isMoving = false;

    private void Start()
    {
        _player = GameObject.Find("CheckPoints")?.GetComponent<PlayerSpawnLocations>();
        if (_player != null)
        {
            TransformPlayer = _player._transformPlayer.transform;
            _targetPosition = endLaser.position;
        }
    }

    private void Update()
    {
        if (_isMoving)
        {
            endLaser.position = Vector3.Lerp(endLaser.position, _targetPosition, Time.deltaTime * smoothSpeed);

            if (Vector3.SqrMagnitude(endLaser.position - _targetPosition) < 0.0001f)
            {
                endLaser.position = _targetPosition;
                _isMoving = false;
            }
        }
    }

    public void LookAt(Vector3 endPoint)
    {
        if (_targetPosition != endPoint)
        {
            _targetPosition = endPoint;
            _isMoving = true;
        }
    }
}

