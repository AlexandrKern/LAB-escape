using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    [SerializeField] Transform pupil;
    [SerializeField] Transform eyeCenter;
    [SerializeField] float radius = 0.25f;

    private Transform _player;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = pupil.localPosition;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            _player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _player = null;
        pupil.localPosition = _startPos;
    }

    void FixedUpdate()
    {
        if (_player != null)
        {
            Vector3 direction = _player.position - eyeCenter.position;
            Vector3 constrainedPosition = direction.normalized * radius;
            pupil.localPosition = constrainedPosition;
        }
    }
}
