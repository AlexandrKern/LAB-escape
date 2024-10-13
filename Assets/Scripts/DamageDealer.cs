using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] protected int damageAmount = 10;

    public virtual void DealDamage(GameObject target)
    {
        CharacterHealth characterHealth = target.GetComponent<CharacterHealth>();
        if (characterHealth != null)
        {
            characterHealth.TakeDamage(damageAmount);
        }
    }

}
