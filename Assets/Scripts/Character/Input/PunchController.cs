using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField]
    private float DamageCircleRadius = 5f;

    [SerializeField]
    private Vector3 DamageCircleOffset = Vector3.zero;

    [Space]
    [SerializeField]
    private bool DebugCircle;

    public void Punch()
    {
        //TODO: animation logic will be here

        CauseDamage();
    }

    private void CauseDamage()
    {
        var overlaped = Physics2D.OverlapCircleAll(transform.TransformPoint(DamageCircleOffset), DamageCircleRadius);
        for (int i = 0; i < overlaped.Length; i++)
        {
            var damageable = overlaped[i].gameObject.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.CauseDamage();
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (DebugCircle)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireSphere(transform.TransformPoint(DamageCircleOffset), DamageCircleRadius);
        }
    }
}