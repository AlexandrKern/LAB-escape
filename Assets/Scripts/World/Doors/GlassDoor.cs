using UnityEngine;

public class GlassDoor : MonoBehaviour, IDamageable
{
    public void CauseDamage()
    {        
        //TODO: destroy animation here
        Destroy(gameObject);
    }
}
