using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorTurnState : IEnemyState
{
    private Interceptor _interceptor;

    public InterceptorTurnState(Interceptor interceptor)
    {
        _interceptor = interceptor;
    }

    public void Enter()
    {
        
        _interceptor.animatorController.animator.SetBool("isTurn",true);
    }

    public void Execute()
    {
        if (!_interceptor.animatorController.isTurn)
        {
            _interceptor.movement.Patrol(_interceptor.eye);
            _interceptor.ChangeState(new InterceptorPatrolState(_interceptor));
        }
    }

    public void Exit()
    {
        _interceptor.animatorController.animator.SetBool("isTurn", false);
    }
}
