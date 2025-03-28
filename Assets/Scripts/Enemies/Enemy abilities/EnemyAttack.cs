using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _hitPrefub;

    [Header("Damage")]
    [SerializeField] private int _meleeDamage = 100;

    [Header("Attack Distances")]
    public float longRangeAttackDistance = 5f;
    public float meleeAttackDistance = 1.5f;

    [Header("Recharge Times")]
    [SerializeField] private float _rechargeLongRangeAttackTime = 7f;
    private float _startRechargeLongRangeAttackTime;

    [SerializeField] private float _rechargeMeleeAttackTime = 5f;
    private float _startRechargeMeleeAttackTime;

    [HideInInspector] public bool toggleRechargeLongRange;
    [HideInInspector] public bool toggleRechargeMelee;

    [Header("Attack Durations")]
    [SerializeField] private float _timeLaserFollowsPlayer = 2f;
    [SerializeField] private float _delayBeforeFiring = 1f;

    private PlayerSpawnLocations _player;
    private GameObject _swarm; 
    private CharacterHealth _characterHealth; 

    private bool _isPlayerInAttackRange;
    public bool isStartLongRangeAttack;

    private void Start()
    {
        _player = GameObject.Find("CheckPoints").GetComponent<PlayerSpawnLocations>();
        _swarm = _player._transformPlayer.gameObject;
        _characterHealth = _swarm.GetComponent<CharacterHealth>();
        _startRechargeLongRangeAttackTime = _rechargeLongRangeAttackTime;
        _startRechargeMeleeAttackTime = _rechargeMeleeAttackTime;
    }

    private void Update()
    {
        HandleRechargeUpdates();
    }

    /// <summary>
    /// ��������� ���������� �������������� ����
    /// </summary>
    private void HandleRechargeUpdates()
    {
        RechargeLongRangeAttack(); 
        RechargeMeleeAttack();
    }

    /// <summary>
    /// ������� �����
    /// </summary>
    /// <param name="interceptor"></param>
    public void InitiateLongRangeAttack(Interceptor interceptor)
    {
        StartCoroutine(LongRangeShot(interceptor)); 
        toggleRechargeLongRange = true;
    }

    /// <summary>
    ///  �������� ��� ���������� �������� ��������
    /// </summary>
    /// <param name="interceptor"></param>
    /// <returns></returns>
    private IEnumerator LongRangeShot(Interceptor interceptor)
    {
        if (isStartLongRangeAttack) yield break;
        isStartLongRangeAttack = true;
        interceptor.animatorController.animator.SetTrigger("LongRangeAttack");
        interceptor.laserMove.isLooking = true; 

        yield return new WaitForSeconds(_timeLaserFollowsPlayer); 
        interceptor.laserMove.isLooking = false;
        interceptor.laserMove.laser.StartBlinking();

        yield return new WaitForSeconds(_delayBeforeFiring);
        interceptor.laserMove.laser.StopBlinking();
        interceptor.laserMove.laser.isColorSwitching = false;
        if ( !interceptor.animatorController.isMeleeAttack)
        {
            InstantiateLaserHit(interceptor);
        }
        isStartLongRangeAttack = false;
    }

    private void InstantiateLaserHit(Interceptor interceptor)
    {
            GameObject go = Instantiate(_hitPrefub, interceptor.laserMove.lastPlayerPosition, Quaternion.identity);
            Destroy(go,0.6f);
    }

    /// <summary>
    /// ������� �����
    /// </summary>
    /// <param name="interceptor"></param>
    public void InitiateMeleeAttack(Interceptor interceptor)
    {
        interceptor.laserMove.laser.StopBlinking();
        interceptor.laserMove.laser.isColorSwitching = false; 
        interceptor.animatorController.animator.SetTrigger("Attack");
        //CheckPlayerInAttackRange(interceptor);
    }

    /// <summary>
    /// ��������� ��������� �� ����� � ���� �����
    /// </summary>
    /// <param name="interceptor"></param>
    private void CheckPlayerInAttackRange(Interceptor interceptor)
    {
        if (interceptor.movement.GetDistanceToPlayer() <= meleeAttackDistance)
        {
            _isPlayerInAttackRange = true; 
        }
        else
        {
            _isPlayerInAttackRange = false;
        }
    }

    /// <summary>
    /// ������� ���� �� ������� �����
    /// </summary>
    public void ApplyMeleeAttackDamage()
    {
        if (Vector3.Distance(transform.position, _swarm.transform.position) <= meleeAttackDistance)
        {
            _characterHealth.TakeDamage(_meleeDamage);
        }
    }

    /// <summary>
    /// ����������� ������� �����
    /// </summary>
    public void RechargeLongRangeAttack()
    {
        if (toggleRechargeLongRange)
        {
            _rechargeLongRangeAttackTime -= Time.deltaTime;
            if (_rechargeLongRangeAttackTime <= 0)
            {
                _rechargeLongRangeAttackTime = _startRechargeLongRangeAttackTime;
                toggleRechargeLongRange = false;
            }
        }
    }

    /// <summary>
    /// ����������� ������� �����
    /// </summary>
    public void RechargeMeleeAttack()
    {
        if (toggleRechargeMelee)
        {
            _rechargeMeleeAttackTime -= Time.deltaTime; 
            if (_rechargeMeleeAttackTime <= 0)
            {
                _rechargeMeleeAttackTime = _startRechargeMeleeAttackTime;
                toggleRechargeMelee = false; 
            }
        }
    }

    public void StopLongRangeAttack()
    {
        isStartLongRangeAttack = false;
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, longRangeAttackDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttackDistance);
    }
}
