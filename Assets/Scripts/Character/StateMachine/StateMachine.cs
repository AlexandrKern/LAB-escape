using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<StateBase> _states = new();
    private Character _context;
    private StateBase _currentState;

    public float InputHorizontal 
    {
        set
        {
            _currentState.InputHorizontal = value;
        }
    }
    public float InputVertical 
    {
        set
        {
            _currentState.InputVertical = value;
        }
    }

    public StateMachine(Character context)
    {
        _context = context;
        _states.Add(new StateBase(_context));
        _states.Add(new StateBase(_context));
        _states.Add(new StateBase(_context));
        _states.Add(new StateBase(_context));
        _currentState = _states[0];
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void Interact()
    {
        _currentState.Interact();
    }

    public void EnterState(int index)
    {
        //TODO: index validity check
        _currentState = _states[index];
    }
}
