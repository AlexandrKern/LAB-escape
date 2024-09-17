using UnityEngine;

[RequireComponent(typeof(Swarm))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(InteractController))]
[RequireComponent(typeof(PunchController))]
[RequireComponent(typeof(SwarmInputHandler))]
public class Character : MonoBehaviour
{
    private StateMachine _stateMachine;
    [HideInInspector]
    public Swarm swarm;
    [HideInInspector]
    public MoveController moveController;
    [HideInInspector]
    public InteractController interactController;
    [HideInInspector]
    public PunchController punchController;
    [HideInInspector]
    public SwarmInputHandler inputHandler;

    public float InputHorizontal 
    {
        set
        {
            _stateMachine.InputHorizontal = value;
        } 
    }
    public float InputVertical 
    {
        set
        {
            _stateMachine.InputVertical = value;
        }
    }

    public void QInteract()
    {
        _stateMachine.QInteract();
    }

    public void EInteract()
    {
        _stateMachine.EInteract();
    }

    public void ChangeForm(FormType form)
    {
        _stateMachine.EnterState(form);
    }
    
    void Awake()
    {
        swarm = GetComponent<Swarm>();
        moveController = GetComponent<MoveController>();
        interactController = GetComponent<InteractController>();
        _stateMachine = new StateMachine(this, FormType.Base);
        punchController = GetComponent<PunchController>();
        inputHandler = GetComponent<SwarmInputHandler>();
    }

    //void Update()
    //{
    //    StateMachineUpdater();
    //}

    public void StateMachineUpdater()
    {
        _stateMachine.Update();
    }
}
