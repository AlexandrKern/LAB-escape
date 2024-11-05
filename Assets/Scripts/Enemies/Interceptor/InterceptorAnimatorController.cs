using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InterceptorAnimatorController : MonoBehaviour
{

    [HideInInspector] public Animator animator;
    [HideInInspector] public bool isMeleeAttack;
    [HideInInspector] public bool isLongRangeAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMeleeAttack()
    {
        isMeleeAttack  = true;
    }

    public void OffMeleeAttack()
    {
        isMeleeAttack = false;
    }

    public bool IsAnimationPlaying(string animName)
    {
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animStateInfo.IsName(animName) && !animator.IsInTransition(0);
    }
}