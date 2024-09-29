using UnityEngine;

public class InterceptorSearchState : IEnemyState
{
    private Interceptor _interceptor;
    private float _detectionTimer;
    private float _searchTime;

    public InterceptorSearchState(Interceptor interceptor)
    {
        _interceptor = interceptor;
    }
    public void Enter()
    {
        _interceptor.movement._camePlaceOfSearch = false;
        _searchTime = 0;
        _detectionTimer = 0;
        Debug.Log("����� �����");
    }

    public void Execute()
    {
        SearchPlayer();

        if (_interceptor.eye.DetectPlayer())
        {
            _detectionTimer += Time.deltaTime;
            if (_detectionTimer >= _interceptor.eye.detectionTimer)
            {
                _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
            }
        }
        else if (_interceptor.movement._camePlaceOfSearch)
        {
            _searchTime += Time.deltaTime;
            Debug.Log(_searchTime);
            if (_searchTime >= _interceptor.movement.searchTime)
            {
                _interceptor.ChangeState(new InterceptorPatrolState(_interceptor));
            }
        } 
    }

    public void Exit()
    {
        Debug.Log("�������� �����");
    }

    public void SearchPlayer()
    {
        _interceptor.movement.MoveTo(_interceptor.ears.LastPlayerPosition);
    }
}
