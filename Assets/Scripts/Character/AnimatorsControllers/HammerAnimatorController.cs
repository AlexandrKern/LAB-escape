using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAnimatorController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    private void FixedUpdate()
    {
        if(rb.velocity.x > 0.5f || rb.velocity.x < -0.5f)
            animator.SetBool("IsMove", true);
        else
            animator.SetBool("IsMove", false);
    }

    public void SetTriggerHitAttack()
    {
        animator.SetTrigger("AttackHit");
    }

    public void SetTriggerJumpAttack()
    {
        animator.SetTrigger("AttackJump");
    }
}
