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

    virtual public void QInteract()
    {
    }

    virtual public void EInteract()
    {
    }

    virtual public void Update()
    {
        _context.moveController.InputHorizontal = InputHorizontal;
    }

    virtual public void OnStateEnter()
    {
        _context.swarm.SetForm(StateForm);
    }

    virtual public void OnStateExit()
    {

    }
}
