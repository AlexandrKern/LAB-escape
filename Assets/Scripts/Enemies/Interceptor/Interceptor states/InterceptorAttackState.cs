
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
    /// ��������� ������������� ��������� �����
    /// </summary>
    /// <param name="isAttacking"></param>
    private void SetAttackAnimationState(bool isAttacking)
    {
        _interceptor.animatorController.animator.SetBool("IsMoving", !isAttacking);
        _interceptor.animatorController.animator.SetBool("IsAttack", isAttacking);
    }

    /// <summary>
    /// �������� ������ �����
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
    /// // ��������� �� ���������� ������ � ������� ������� �����
    /// </summary>
    /// <returns></returns>
    private bool IsWithinMeleeAttackRange()
    {
        return _interceptor.movement.GetDistanceToPlayer() <= _interceptor.attack.meleeAttackDistance;
    }

    /// <summary>
    /// // ��������� �� ���������� ������ � ������� ������������ �����
    /// </summary>
    /// <returns></returns>
    private bool IsWithinLongRangeAttackRange()
    {
        return _interceptor.movement.GetDistanceToPlayer() <= _interceptor.attack.longRangeAttackDistance;
    }

    /// <summary>
    /// ������������ � ������� ������ ��� ���� ��������� ��������
    /// </summary>
    private void MoveToPlayer()
    {
        _interceptor.movement.MoveTo(_interceptor.movement.transformPlayer.position, _interceptor.eye);
        _interceptor.movement.StopMovement();
    }

    /// <summary>
    /// ������� �����
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
    /// ������ �� �������
    /// </summary>
    private void AimAndShootLaser()
    {
        _interceptor.laserMove.LoocAtPlayer(_interceptor.eye.DetectPlayer());
    }

    /// <summary>
    /// ������� �����
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
    ///  ������� � ����� �������������
    /// </summary>
    private void ResetSpeedAndChase()
    {
        _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    private void StopColorSwitching()
    {
        _interceptor.laserMove.laser.StopBlinking();
        _interceptor.laserMove.laser.isColorSwitching = false;
    }
}


