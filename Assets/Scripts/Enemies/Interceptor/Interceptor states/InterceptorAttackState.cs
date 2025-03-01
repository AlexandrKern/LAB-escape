
using UnityEngine;

public class InterceptorAttackState : IEnemyState
{
    private Interceptor _interceptor;

    public InterceptorAttackState(Interceptor interceptor)
    {
        _interceptor = interceptor;
    }

    public void Enter()
    {
        SetAttackAnimationState(true);
    }

    public void Execute()
    {
        PerformAttackLogic();
    }

    public void Exit()
    {
        _interceptor.attack.StopLongRangeAttack();
        StopColorSwitching();
        SetAttackAnimationState(false);
    }

    /// <summary>
    /// Настройка анимационного состояния атаки
    /// </summary>
    /// <param name="isAttacking"></param>
    private void SetAttackAnimationState(bool isAttacking)
    {
        _interceptor.animatorController.animator.SetBool("IsMoving", !isAttacking);
        _interceptor.animatorController.animator.SetBool("IsAttack", isAttacking);
    }

    /// <summary>
    /// Основная логика атаки
    /// </summary>
    private void PerformAttackLogic()
    {
        if(!_interceptor.eye.DetectPlayer())
        {
            _interceptor.ChangeState(new InterceptorPatrolState(_interceptor));
        }
        if (IsWithinMeleeAttackRange())
        {
            MoveToPlayer();
            _interceptor.movement.StopMovement();
            PerformMeleeAttack();
        }

        else if (IsWithinLongRangeAttackRange()&& _interceptor.eye.DetectPlayer())
        {
            _interceptor.movement.StopMovement();
            PerformLongRangeAttack();
        }
        if (_interceptor.attack.isStartLongRangeAttack)
        {
            
            AimAndShootLaser();
        }
        if(!IsWithinMeleeAttackRange()&& !IsWithinLongRangeAttackRange()&& !_interceptor.attack.isStartLongRangeAttack)
        {
            ResetSpeedAndChase();
        }

    }

    /// <summary>
    /// // Проверяет на нахождение игрока в радиусе ближней атаки
    /// </summary>
    /// <returns></returns>
    private bool IsWithinMeleeAttackRange()
    {
        return _interceptor.movement.GetDistanceToPlayer() <= _interceptor.attack.meleeAttackDistance;
    }

    /// <summary>
    /// // Проверяет на нахождение игрока в радиусе дальнобойной атаки
    /// </summary>
    /// <returns></returns>
    private bool IsWithinLongRangeAttackRange()
    {
        return _interceptor.movement.GetDistanceToPlayer() <= _interceptor.attack.longRangeAttackDistance;
    }

    /// <summary>
    /// Поворачивает в сторону игрока при этом сбрасывая скорость
    /// </summary>
    private void MoveToPlayer()
    {
        _interceptor.movement.MoveTo(_interceptor.movement.transformPlayer.position, _interceptor.eye);
        _interceptor.movement.StopMovement();
    }

    /// <summary>
    /// Ближняя атака
    /// </summary>
    private void PerformMeleeAttack()
    {
        _interceptor.attack.StopLongRangeAttack();
        if (!_interceptor.attack.toggleRechargeMelee)
        {
            _interceptor.attack.InitiateMeleeAttack(_interceptor);
            _interceptor.attack.toggleRechargeMelee = true;
        }
    }

    /// <summary>
    /// Следит за игроком
    /// </summary>
    private void AimAndShootLaser()
    {
        _interceptor.laserMove.LoocAtPlayer(_interceptor.eye.DetectPlayer());
    }

    /// <summary>
    /// Дальняя атака
    /// </summary>
    private void PerformLongRangeAttack()
    {
        if (_interceptor.animatorController.isMeleeAttack)
        {
            StopColorSwitching();
        }

        if (!_interceptor.attack.toggleRechargeLongRange && !_interceptor.animatorController.isMeleeAttack)
        {
            _interceptor.attack.InitiateLongRangeAttack(_interceptor);
            _interceptor.attack.toggleRechargeLongRange = true;
        }
    }

    /// <summary>
    ///  Переход в режим преследования
    /// </summary>
    private void ResetSpeedAndChase()
    {
        _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
    }

    /// <summary>
    /// Выключает лазер
    /// </summary>
    private void StopColorSwitching()
    {
        _interceptor.laserMove.laser.StopBlinking();
        _interceptor.laserMove.laser.isColorSwitching = false;
    }
}


