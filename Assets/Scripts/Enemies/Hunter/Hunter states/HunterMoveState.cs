
public class HunterMoveState : IEnemyState
{
    private Hunter _hunter;

    public HunterMoveState(Hunter hunter)
    {
        _hunter = hunter;
    }

    public void Enter()
    {
       
    }

    public void Execute()
    {

        _hunter.movement.Move();
        if (_hunter.movement.GetDistanceToPlayer() <= 0.5f)
        {
            _hunter.ChangeState(new HunterAttackState(_hunter));
        }

    }

    public void Exit()
    {
        _hunter.movement.ClearPath();
    }
}
