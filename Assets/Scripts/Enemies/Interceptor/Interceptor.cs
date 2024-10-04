using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyEars))]
[RequireComponent(typeof(EnemyEye))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Interceptor : MonoBehaviour
{
   
    private IEnemyState currentState;

    [HideInInspector] public EnemyEye eye;
    [HideInInspector] public EnemyEars ears;
    [HideInInspector] public EnemyMovement movement;
    [HideInInspector] public EnemyAttack attack;
    [HideInInspector] public EnemyHealth health;

    [SerializeField] private InterceptorStates state;
    [HideInInspector] public bool isActiv = true;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
        attack = GetComponent<EnemyAttack>();
        movement = GetComponent<EnemyMovement>();
        eye = GetComponent<EnemyEye>();
        ears = GetComponent<EnemyEars>();
        StartState(state);
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
                isActiv = false;
                break;
            case InterceptorStates.PatrolState:
                ChangeState(new InterceptorPatrolState(this));
                break;
            default:
                break;
        }
    }
}