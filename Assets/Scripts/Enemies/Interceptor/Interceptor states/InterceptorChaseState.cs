public class InterceptorChaseState : IEnemyState
{
    private Interceptor _interceptor;
    private float _speed;
   
    public InterceptorChaseState(Interceptor interceptor)
    {
        _interceptor = interceptor;
    }

    public void Enter()
    {
        _speed = _interceptor.movement.speed;
        _interceptor.movement.speed = _interceptor.movement.chaseSpeed;
        _interceptor.animatorController.animator.SetBool("IsMoving", true);
    }

    public void Execute()
    {
        ChasePlayer();

        if (_interceptor.movement.GetDistanceToPlayer() >= _interceptor.eye.viewRadius)
        {
             _interceptor.ChangeState(new InterceptorIdleState(_interceptor));
        }
        if (_interceptor.movement.GetDistanceToPlayer() <= _interceptor.attack.longRangeAttackDistance)
        {
            _interceptor.ChangeState(new InterceptorAttackState(_interceptor));
        }
    }

    public void Exit()
    {
        _interceptor.movement.speed = _speed;
        _interceptor.animatorController.animator.SetBool("IsMoving", false);
    }

    private void ChasePlayer()
    {
         _interceptor.movement.MoveTo(_interceptor.movement.transformPlayer.position,_interceptor.eye);
    }
}

