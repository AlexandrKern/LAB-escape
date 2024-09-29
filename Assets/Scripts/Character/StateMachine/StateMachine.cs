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
        AddStates();
        EnterState(defaultState);
    }

    public void UpdateStatesCollection()
    {
        _states.Clear();
        AddStates();
    }

    private void AddStates()
    {
        _states.Add(FormType.Base, new BaseForm(_context));

        if (Data.IsAnthropomorphicFormAvailable)
            _states.Add(FormType.Anthropomorphic, new AnthropomorphicForm(_context));
        if (Data.IsHammerFormAvailable)
            _states.Add(FormType.Hammer, new HammerForm(_context));
        if (Data.IsBurglarFormAvailable)
            _states.Add(FormType.Burglar, new BurglarForm(_context));
        if (Data.IsMirrorFormAvailable)
            _states.Add(FormType.Mirror, new MirrorForm(_context));
        if (Data.IsMimicryFormAvailable)
            _states.Add(FormType.Mimicry, new MimicryForm(_context));
    }


    public void Update()
    {
        _currentState.Update();
    }

    public void QInteract()
    {
        _currentState.QInteract();
    }

    public void EInteract()
    {
        _currentState.EInteract();
    }

    public void EnterState(FormType form)
    {
        if(_currentState != null)
        {
            if(_currentState.StateForm == form)
            {
                return;
            }
            _currentState.OnStateExit();
        }

        if (_states.TryGetValue(form, out StateBase nextState))
        {
            _currentState = _states[form];
            _currentState.OnStateEnter();
        }
    }
}
