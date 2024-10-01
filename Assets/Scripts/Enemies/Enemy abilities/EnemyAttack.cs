using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDistance = 1.5f;

    public void Attack()
    {
        //Debug.Log("Враг атакует");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
