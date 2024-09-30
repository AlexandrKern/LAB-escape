using UnityEngine;

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

    private void Start()
    {
        attack = GetComponent<EnemyAttack>();
        movement = GetComponent<EnemyMovement>();
        eye = GetComponent<EnemyEye>();
        ears = GetComponent<EnemyEars>();
        ChangeState(new InterceptorPatrolState(this));
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
}