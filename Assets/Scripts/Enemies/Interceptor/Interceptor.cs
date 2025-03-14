using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyEars))]
[RequireComponent(typeof(EnemyEye))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InterceptorAnimatorController))]
public class Interceptor : MonoBehaviour
{
   
    private IEnemyState currentState;

    [HideInInspector] public InterceptorAnimatorController animatorController;
    [HideInInspector] public EnemyEye eye;
    [HideInInspector] public EnemyEars ears;
    [HideInInspector] public EnemyMovement movement;
    [HideInInspector] public EnemyAttack attack;
    [HideInInspector] public EnemyHealth health;
    private SpriteRenderer spriteRenderer;

    public InterceptorLaserMove laserMove;
    [SerializeField] private InterceptorStates state;
    [HideInInspector] public bool isActiv = true;

    [SerializeField] private Transform _circle;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animatorController = GetComponent<InterceptorAnimatorController>();
        health = GetComponent<EnemyHealth>();
        attack = GetComponent<EnemyAttack>();
        movement = GetComponent<EnemyMovement>();
        eye = GetComponent<EnemyEye>();
        ears = GetComponent<EnemyEars>();
        StartState(state);
        CheckIsActiv();
        
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    private void StartState(InterceptorStates state)
    {
        switch (state)
        {
            case InterceptorStates.WaitState:
                ChangeState(new InterceptorWaitState(this));
                isActiv = Data.IsHammerFormAvailable;
                break;
            case InterceptorStates.PatrolState:
                ChangeState(new InterceptorPatrolState(this));
                break;
            default:
                break;
        }
    }

    public void CheckIsActiv()
    {
        if (isActiv)
        {
            if (_circle != null)
            {
                _circle.gameObject.SetActive(false);
                animatorController.animator.SetTrigger("Activate");
                spriteRenderer.sortingOrder = 6;

            }
           
        }   
    }
}