using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    protected Character _context; 
    public float InputHorizontal { get; set; }
    public float InputVertical { get; set; }

    virtual public FormType StateForm => FormType.Base;

    public StateBase(Character context)
    {
        _context = context;
    }

    virtual public void Interact()
    {
        //TODO: handle the end of interaction callback
        _context._interactController.Interact();
    }

    virtual public void Update()
    {
        _context._moveController.InputHorizontal = InputHorizontal;
    }

    virtual public void OnStateEnter()
    {
        _context._swarm.SetForm(StateForm);
    }

    virtual public void OnStateExit()
    {

    }
}
