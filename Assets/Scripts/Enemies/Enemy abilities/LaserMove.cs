using UnityEngine;

public class LaserMove : MonoBehaviour
{
    [HideInInspector] public Laser laser;

    [SerializeField] private Transform endLaser;

    private Vector3 _laserEndStartPosition;

    private bool _isLoocAtPlayer;

    public Transform laserTransform;

    private PlayerSpawnLocations _player;
   [HideInInspector] public Transform transformPlayer;

    private void Start()
    {
        _player = GameObject.Find("CheckPoints").GetComponent<PlayerSpawnLocations>();
        transformPlayer = _player._transformPlayer.transform;
        laserTransform = transform;
        laser = GetComponent<Laser>();
        _isLoocAtPlayer = false;
        _laserEndStartPosition = endLaser.localPosition;
    }

    public void SetLaserPos(bool movingRight)
    {
        if (_isLoocAtPlayer)
        {
            endLaser.localPosition = _laserEndStartPosition;
            _isLoocAtPlayer = false;
            laser.playerVisible = false;
            
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

    public void LoocAtPlayer()
    {
        laser.playerVisible = true;
        endLaser.position = transformPlayer.position;
        _isLoocAtPlayer = true;
    }

    public void SetLaserDistance()
    {
        float dictance = Vector3.Distance(laserTransform.position, transformPlayer.position);
        laser.maxDistance = dictance;
    }
}
