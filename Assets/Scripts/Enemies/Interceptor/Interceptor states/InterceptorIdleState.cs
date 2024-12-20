using UnityEngine;

public class InterceptorIdleState : IEnemyState
{
    private Interceptor _interceptor;
    private float _idleTimer; 
    private float _detectionTimer;

    public InterceptorIdleState(Interceptor interceptor)
    {
        _interceptor = interceptor;
    }

    public void Enter()
    {
        _interceptor.animatorController.animator.SetBool("IsMoving", false);
        _idleTimer = 0f;
        _detectionTimer = 0f;
        _interceptor.laserMove.laser.StopBlinking();
    }

    public void Execute()
    {
        Idle();
        if (_interceptor.eye.DetectPlayer())
        {
            _detectionTimer += Time.deltaTime;
            if (_detectionTimer >= _interceptor.eye.detectionTimer)
            {
                _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
            }
        }
        if (_interceptor.ears.Hear)
        {
            _interceptor.ChangeState(new InterceptorSearchState(_interceptor));
        }
    }

    public void Exit()
    {

    }

    private void Idle()
    {
        _idleTimer += Time.deltaTime;
        if (_idleTimer >= _interceptor.movement.idleTimer)
        {
            _interceptor.ChangeState(new InterceptorTurnState(_interceptor));
        }
    }
}

