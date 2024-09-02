using UnityEngine;

public class MechanicalDoor : MonoBehaviour, IDamageable
{
    public void CauseDamage()
    {
        //TODO: destroy animation here
        Destroy(gameObject);
    }
}
