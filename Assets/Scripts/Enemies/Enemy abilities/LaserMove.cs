using UnityEngine;

public class LaserMove : MonoBehaviour
{
    [HideInInspector] public Laser laser;
    [SerializeField] private Transform endLaser;
    private Vector3 _laserEndStartPosition;
    public Transform laserTransform;
    private PlayerSpawnLocations _player;
    [HideInInspector] public Transform transformPlayer;
    [HideInInspector] public bool isLooking;
    private bool _isLoocAtPlayer;
    public Vector3 lastPlayerPosition;

    private void Start()
    {
        _player = GameObject.Find("CheckPoints").GetComponent<PlayerSpawnLocations>();
        transformPlayer = _player._transformPlayer.transform;
        laserTransform = transform;
        laser = GetComponent<Laser>();
        _laserEndStartPosition = endLaser.localPosition;
    }

    /// <summary>
    /// Поворачивает лазер
    /// </summary>
    public void SetLaserPos(bool movingRight)
    {
        if (_isLoocAtPlayer)
        {
            endLaser.localPosition = _laserEndStartPosition;
            _isLoocAtPlayer = false;
            
        }

        Vector3 vec = endLaser.localPosition;

        if (movingRight)
        {
            endLaser.localPosition = new Vector3(Mathf.Abs(vec.x), vec.y, vec.z);
        }
        else
        {
            endLaser.localPosition = new Vector3(-Mathf.Abs(vec.x), vec.y, vec.z);
        }
    }

    /// <summary>
    /// Следит за игроком
    /// </summary>
    public void LoocAtPlayer(bool isDetected)
    {
        if (isLooking && isDetected)
        {
            SetLaserDistance(transformPlayer.position);
            endLaser.position = transformPlayer.position;
            lastPlayerPosition = transformPlayer.position;
            laser.isColorSwitching = true;
        }
        else
        {
            endLaser.position = lastPlayerPosition;
            SetLaserDistance(lastPlayerPosition);
        }
    }

    /// <summary>
    /// Устанавливает дальность лазера
    /// </summary>
    public void SetLaserDistance(Vector3 endPosition)
    {
        float dictance = Vector3.Distance(laserTransform.position, endPosition);
        laser.maxDistance = dictance;
    }
}
