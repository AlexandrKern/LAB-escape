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
        //Debug.Log("Враг в состоянии стоять");
        _idleTimer = 0f;
        _detectionTimer = 0f;
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
        //Debug.Log("Враг закончил стоять");
    }

    private void Idle()
    {
        _idleTimer += Time.deltaTime;
        if (_idleTimer >= _interceptor.movement.idleTimer)
        {
            _interceptor.ChangeState(new InterceptorPatrolState(_interceptor));
        }
    }
}

