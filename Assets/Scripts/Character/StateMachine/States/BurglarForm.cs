using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurglarForm : StateBase
{
    public override FormType StateForm => FormType.Burglar;

    public BurglarForm(Character context) : base(context)
    {
    }

    public override void EInteract()
    {
        _context.interactController.Interact<IInteractableTerminal>();
    }
}
