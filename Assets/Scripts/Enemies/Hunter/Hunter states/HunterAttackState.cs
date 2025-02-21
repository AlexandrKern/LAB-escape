using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttackState : IEnemyState
{
    private Hunter _hunter;

    public HunterAttackState(Hunter hunter)
    {
        _hunter = hunter;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        Debug.Log("Атакую игрока");
        if (_hunter.movement.GetDistanceToPlayer() >= 1)
        {
            _hunter.ChangeState(new HunterMoveState(_hunter));
        }
    }

    public void Exit()
    {
        
    }

  
}
