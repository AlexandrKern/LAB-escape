using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MimicryForm : StateBase
{
    protected override FormType StateForm => FormType.Mimicry;

    public MimicryForm(Character context) : base(context)
    {
    }
}
