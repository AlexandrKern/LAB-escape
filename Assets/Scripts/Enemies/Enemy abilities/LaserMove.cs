using UnityEngine;

public class LaserMove : MonoBehaviour
{
    private Laser laser;

    [SerializeField] private Transform endLaser;

    private Vector3 _laserEndStartPosition;

    private bool _isLoocAtPlayer;


    private void Start()
    {
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

    public void LoocAtPlayer(Transform direction)
    {
        laser.playerVisible = true;
        endLaser.position = direction.position;
        _isLoocAtPlayer = true;
    }
}
