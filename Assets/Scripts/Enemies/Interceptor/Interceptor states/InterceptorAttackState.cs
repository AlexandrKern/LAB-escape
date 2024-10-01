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
        //Debug.Log("Начал атаку");
    }

    public void Execute()
    {
        AttackPlayer();

        if (_interceptor.movement.GetDistanceToPlayer() >= _interceptor.attack.attackDistance)
        {
            _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
        }
    }

    public void Exit()
    {
        //Debug.Log("Закончил атаку");
    }

    private void AttackPlayer()
    {
        _interceptor.attack.Attack();
       
    }
}

