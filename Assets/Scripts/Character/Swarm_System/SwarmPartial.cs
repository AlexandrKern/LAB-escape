using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Swarm : MonoBehaviour
{
    [SerializeField]
    private List<FormType> formOrder;

    public void SetForm(FormType form)
    {
        int index = formOrder.IndexOf(form);
        if(index == -1)
        {
            throw new System.Exception($"Swarm: Form \"{form}\" not found.");
        }
        SetFormIndex(index);
    }
}
