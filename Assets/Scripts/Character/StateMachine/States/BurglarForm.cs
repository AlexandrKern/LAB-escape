using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurglarForm : StateBase
{
    protected override FormType StateForm => FormType.Burglar;

    public BurglarForm(Character context) : base(context)
    {
    }
}
