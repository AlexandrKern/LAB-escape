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
        
    }

    public void Execute()
    {
        if (_interceptor.movement.GetDistanceToPlayer()<= _interceptor.attack.meleeAttackDistance)
        {
            if (!_interceptor.attack.toggleRechargeMelee)
            {
                _interceptor.attack.toggleRechargeMelee = true;
                _interceptor.attack.MeleeAttack(_interceptor);
            }
            _interceptor.movement.MoveTo(_interceptor.movement.transformPlayer.position, _interceptor.eye);

        }
        else if (_interceptor.movement.GetDistanceToPlayer() <= _interceptor.attack.longRangeAttackDistance)
        {
            _interceptor.laserMove.SetLaserDistance();
            if (!_interceptor.attack.toggleRechargeLongRange)
            {
                _interceptor.attack.toggleRechargeLongRange = true;
                _interceptor.attack.LongRangeAttack(_interceptor);
            }
        }
        else
        {
            _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
        }
    }

    public void Exit()
    {
        _interceptor.laserMove.laser.ResetWidth();
        _interceptor.laserMove.laser.ResetDistance();
    }

    private void AttackPlayer()
    {
        
       
    }
}

