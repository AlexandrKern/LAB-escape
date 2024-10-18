using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float longRangeAttackDistance = 5f;
    public float meleeAttackDistance = 1.5f;

    public float rechargeLongRangeAttackTime = 30f;
    [HideInInspector] public float startRechargeLongRangeAttackTime;

    public float rechargeMeleeAttackTime = 3f;
    [HideInInspector] public float startRechargeMeleeAttackTime;



    [HideInInspector] public bool isAimed;

    [HideInInspector] public bool toggleRechargeLongRange;
    [HideInInspector] public bool toggleRechargeMelee;

    private void Start()
    {
        startRechargeLongRangeAttackTime = rechargeLongRangeAttackTime;
        startRechargeMeleeAttackTime = rechargeMeleeAttackTime;
    }

    private void Update()
    {
        RechargeLongRangeAttack();
        RechargeMeleeAttack();
    }

    public void LongRangeAttack(Interceptor interceptor)
    {

            interceptor.laserMove.laser.endWidth = 0.1f ;
     
        Debug.Log("ƒальн€€ атака");
    }

    public void MeleeAttack(Interceptor interceptor)
    {
        interceptor.laserMove.laser.ResetDistance();
        interceptor.laserMove.laser.ResetWidth();
        Debug.Log("Ѕлижн€€ атака");
    }
    public void RechargeLongRangeAttack()
    {

        if (toggleRechargeLongRange)
        {
            rechargeLongRangeAttackTime -= Time.deltaTime;
            if (rechargeLongRangeAttackTime <= 0)
            {
                rechargeLongRangeAttackTime = startRechargeLongRangeAttackTime;
                toggleRechargeLongRange = false;
            }
        }


    }
    public void RechargeMeleeAttack()
    {
        if (toggleRechargeMelee)
        {
            rechargeMeleeAttackTime -= Time.deltaTime;
            if (rechargeMeleeAttackTime <= 0)
            {
                rechargeMeleeAttackTime = startRechargeMeleeAttackTime;
                toggleRechargeMelee = false;
            }
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, longRangeAttackDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeAttackDistance);
    }

}
