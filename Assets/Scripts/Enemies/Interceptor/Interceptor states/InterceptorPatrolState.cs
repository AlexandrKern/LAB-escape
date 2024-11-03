using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorPatrolState : IEnemyState
{
    private Interceptor _interceptor;
    private float _timer;

    public InterceptorPatrolState(Interceptor interceptor)
    {
        
        _interceptor = interceptor;
    }

    public void Enter()
    {
        _timer = 0;
        _interceptor.animatorController.animator.SetBool("IsMoving",true);
    }

    public void Execute()
    {
        Patrol();

        if (_interceptor.eye.DetectPlayer())
        {
            _timer += Time.deltaTime;
            if (_timer >= _interceptor.eye.detectionTimer)
            {
                _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
            }
        }
        else
        {
            _timer = 0;
        }
        if (_interceptor.ears.Hear)
        {
            _interceptor.ChangeState(new InterceptorSearchState(_interceptor));
        }

    }

    public void Exit()
    {
        _interceptor.animatorController.animator.SetBool("IsMoving", false);
    }

    private void Patrol()
    {
       if(_interceptor.movement.Patrol(_interceptor.eye))
       {
           _interceptor.ChangeState(new InterceptorIdleState(_interceptor));
       }
    }
}
