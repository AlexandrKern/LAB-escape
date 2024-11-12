using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int _meleeDamage = 100;
    [SerializeField] private int _longRangeDamage = 100;

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
    /// Обработка обновления восстановления атак
    /// </summary>
    private void HandleRechargeUpdates()
    {
        RechargeLongRangeAttack(); 
        RechargeMeleeAttack();
    }

    /// <summary>
    /// Дальняя атака
    /// </summary>
    /// <param name="interceptor"></param>
    public void InitiateLongRangeAttack(Interceptor interceptor)
    {
        StartCoroutine(LongRangeShot(interceptor)); 
        toggleRechargeLongRange = true;
    }

    /// <summary>
    ///  Корутина для выполнения дальнего выстрела
    /// </summary>
    /// <param name="interceptor"></param>
    /// <returns></returns>
    private IEnumerator LongRangeShot(Interceptor interceptor)
    {
        interceptor.animatorController.animator.SetTrigger("LongRangeAttack");
        interceptor.laserMove.isLooking = true; 

        yield return new WaitForSeconds(_timeLaserFollowsPlayer); 
        interceptor.laserMove.isLooking = false;
        interceptor.laserMove.laser.StartBlinking();

        yield return new WaitForSeconds(_delayBeforeFiring);
        interceptor.laserMove.laser.StopBlinking();
        interceptor.laserMove.laser.isColorSwitching = false;
        if (interceptor.laserMove.laser.isPlayerVisible && !interceptor.animatorController.isMeleeAttack)
        {
            _characterHealth.TakeDamage(_longRangeDamage);
        }
    }

    /// <summary>
    /// Ближняя атака
    /// </summary>
    /// <param name="interceptor"></param>
    public void InitiateMeleeAttack(Interceptor interceptor)
    {
        interceptor.laserMove.laser.StopBlinking();
        interceptor.laserMove.laser.isColorSwitching = false; 
        interceptor.animatorController.animator.SetTrigger("Attack"); 
        CheckPlayerInAttackRange(interceptor); 
    }

    /// <summary>
    /// Проверяет находится ли игрок в зоне атаки
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
    /// Наносит урон от ближней атаки
    /// </summary>
    public void ApplyMeleeAttackDamage()
    {
        if (_isPlayerInAttackRange)
        {
            _characterHealth.TakeDamage(_meleeDamage);
        }
    }

    /// <summary>
    /// Перезарядка дальней атаки
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
    /// Перезарядка ближней атаки
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, longRangeAttackDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttackDistance);
    }
}
