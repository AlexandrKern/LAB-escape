using System.Collections;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField]
    private float DamageCircleRadius = 5f;
    [SerializeField]
    private Vector3 DamageCircleOffset = Vector3.zero;
    [SerializeField]
    private float CameraShakeTime = 0.2f;
    [SerializeField]
    private float CameraShakeSpeed = 30f;
    [SerializeField]
    private ParticleSystem punchParticleSystem;
    [SerializeField]
    private MoveController moveController;

    [Space]
    [SerializeField]
    private bool DebugCircle;

    [SerializeField] HammerAnimatorController AnimatorController;

    public void JumpPunch()
    {
        AnimatorController.SetTriggerJumpAttack();
        moveController.JumpForward();
    }

    public void Punch()
    {
        AnimatorController.SetTriggerHitAttack();
    }

    public void PunchEvent()
    {
        Utils.ShakeCamera(CameraShakeTime, CameraShakeSpeed);
        if(CauseDamage())
        {
            punchParticleSystem.Play();
        }
    }

    public void JumpPunchEvent()
    {
        Utils.ShakeCamera(CameraShakeTime, CameraShakeSpeed);
        punchParticleSystem.Play();
        CauseDamage();
    }

    private bool CauseDamage()
    {
        var overlaped = Physics2D.OverlapCircleAll(transform.TransformPoint(DamageCircleOffset), DamageCircleRadius);
        for (int i = 0; i < overlaped.Length; i++)
        {
            var damageable = overlaped[i].gameObject.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.CauseDamage();
                return true;
            }
        }
        return false;
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
