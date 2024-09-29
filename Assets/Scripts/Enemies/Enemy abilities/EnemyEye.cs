using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    [Range(0, 360)]
    public float viewAngle = 45f;
    public float viewRadius = 5f;
    public float detectionTimer = 2f;
    private float viewDirection = 0f; 

    public LayerMask playerMask; 
    public LayerMask obstacleMask; 

    private Transform _transformPlayer; 
    private Transform _transform; 

    private void Start()
    {
        _transform = transform; 
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    /// <summary>
    /// Метод для обнаружения игрока
    /// </summary>
    /// <returns></returns>
    public bool DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(_transform.position, _transformPlayer.position); 

        if (distanceToPlayer <= viewRadius && IsPlayerInFieldOfView() && !IsPlayerBehindObstacle())
        {
            return true; 
        }
        return false; 
    }

    private bool IsPlayerInFieldOfView()
    {
        Vector2 directionToPlayer = (_transformPlayer.position - _transform.position).normalized; 
        float angleToPlayer = Vector2.Angle(DirFromAngle(viewDirection, true), directionToPlayer); 

        return angleToPlayer <= viewAngle / 2f; 
    }

    private bool IsPlayerBehindObstacle()
    {
        Vector2 directionToPlayer = (_transformPlayer.position - _transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(_transform.position, _transformPlayer.position);

        RaycastHit2D hit = Physics2D.Raycast(_transform.position, directionToPlayer, distanceToPlayer, obstacleMask);
        return hit.collider != null; 
    }

    /// <summary>
    /// Меняет напраление конуса обзора
    /// </summary>
    /// <param name="movingRight"></param>
    public void ChangeDirectionView(bool movingRight)
    {
        viewDirection = movingRight ? 0 : 180f; 
        Debug.Log(movingRight ? "Направо" : "Налево");
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

        Vector3 viewAngleA = DirFromAngle(viewDirection - viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewDirection + viewAngle / 2, false);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius); 
    }
}
