using UnityEngine;

public class InterceptorSearchState : IEnemyState
{
    private Interceptor _interceptor;
    private float _detectionTimer;
    private float _searchTime;
    private float _maxSearchTime;

    public InterceptorSearchState(Interceptor interceptor)
    {
        _interceptor = interceptor;
    }

    public void Enter()
    {
        _interceptor.movement.camePlaceOfSearch = false;
        _searchTime = 0;
        _detectionTimer = 0;
        _maxSearchTime = 0;
    }

    public void Execute()
    {
        SearchPlayer();
        if (_interceptor.eye.DetectPlayer())
        {
            Debug.Log("Увидел " );
            _detectionTimer += Time.deltaTime;
            if (_detectionTimer >= _interceptor.eye.detectionTimer)
            {
                _interceptor.ChangeState(new InterceptorChaseState(_interceptor));
            }
        }
        else if (_interceptor.movement.camePlaceOfSearch)
        {
            _searchTime += Time.deltaTime;
            Debug.Log("Время поиска " + _searchTime);
            if (_searchTime >= _interceptor.movement.searchTime)
            {
                _interceptor.ChangeState(new InterceptorPatrolState(_interceptor));
            }
        }
        else
        {
            _maxSearchTime += Time.deltaTime;
            Debug.Log("Максимальное время поиска " + _maxSearchTime);
            if (_maxSearchTime >= 20)
            {
                _interceptor.ChangeState(new InterceptorPatrolState(_interceptor));
            }
        }
    }

    public void Exit()
    {

    }

    public void SearchPlayer()
    {
        _interceptor.movement.MoveTo(_interceptor.ears.LastPlayerPosition, _interceptor.eye);
    }
}
