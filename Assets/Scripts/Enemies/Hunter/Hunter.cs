using UnityEngine;

[RequireComponent(typeof(EnemyEye))]

public class Hunter : MonoBehaviour
{
    private IEnemyState currentState;

    [HideInInspector] public EnemyEye eye;
    [HideInInspector] public MoveHunter movement;

    private void Start()
    {
        eye = GetComponent<EnemyEye>();
        movement = GetComponent<MoveHunter>();
        ChangeState(new HunterMoveState(this));
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
