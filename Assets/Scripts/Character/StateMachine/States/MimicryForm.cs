using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MimicryForm : StateBase
{
    public override FormType StateForm => FormType.Mimicry;

    public MimicryForm(Character context) : base(context)
    {
    }

    public override void EInteract()
    {
        _context.interactController.Interact<IInteractableTerminal>();
    }
}
