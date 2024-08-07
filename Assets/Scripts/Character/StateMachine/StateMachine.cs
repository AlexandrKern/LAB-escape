using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<FormType, StateBase> _states = new();
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

    public StateMachine(Character context, FormType defaultState)
    {
        _context = context;
        _states.Add(FormType.Base, new BaseForm(_context));
        _states.Add(FormType.Anthropomorphic, new AnthropomorphicForm(_context));
        _states.Add(FormType.Hammer, new HammerForm(_context));
        _states.Add(FormType.Burglar, new BurglarForm(_context));
        _states.Add(FormType.Mirror, new MirrorForm(_context));
        _states.Add(FormType.Mimicry, new MimicryForm(_context));
        EnterState(defaultState);
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void Interact()
    {
        _currentState.Interact();
    }

    public void EnterState(FormType form)
    {
        _currentState?.OnStateExit();
        _currentState = _states[form];
        _currentState.OnStateEnter();
    }
}
