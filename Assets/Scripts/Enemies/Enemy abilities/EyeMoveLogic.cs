using UnityEngine;
using DG.Tweening;

public class EyeFollow : MonoBehaviour
{
    [SerializeField] Transform pupil;
    [SerializeField] Transform eyeCenter;
    [SerializeField] float radius = 0.25f;
    [SerializeField] float moveDuration = 0.3f; 

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
            Vector3 direction = _player.position - eyeCenter.position;
            Vector3 constrainedPosition = direction.normalized * radius;
            pupil.DOLocalMove(constrainedPosition, moveDuration).SetEase(Ease.InOutSine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = null;

            pupil.DOLocalMove(_startPos, moveDuration).SetEase(Ease.InOutSine);
        }
    }

    void FixedUpdate()
    {
        if (_player != null)
        {
            Vector3 direction = _player.position - eyeCenter.position;
            Vector3 constrainedPosition = direction.normalized * radius;
            pupil.DOLocalMove(constrainedPosition, moveDuration).SetEase(Ease.Linear);
        }
    }
}
