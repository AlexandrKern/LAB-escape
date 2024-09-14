using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour, IDamageable
{
    public void CauseDamage()
    {
        //TODO: destroy animation here
        Destroy(gameObject);
    }
}
