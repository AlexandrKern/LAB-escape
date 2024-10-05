using UnityEngine;

public class InterceptorWaitState : IEnemyState
{
    Interceptor Interceptor;

    public InterceptorWaitState(Interceptor interceptor)
    {
        Interceptor = interceptor;
    }
    public void Enter()
    {
        
    }

    public void Execute()
    {
        Wait();
    }

    public void Exit()
    {
        
    }

    private void Wait()
    {
        if (Interceptor.isActiv)
        {
            Interceptor.ChangeState(new InterceptorPatrolState(Interceptor));
        }
    }

}
