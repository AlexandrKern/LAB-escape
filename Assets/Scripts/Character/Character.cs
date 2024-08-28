using UnityEngine;

[RequireComponent(typeof(Swarm))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(InteractController))]
public class Character : MonoBehaviour
{
    private StateMachine _stateMachine;
    [HideInInspector]
    public Swarm _swarm;
    [HideInInspector]
    public MoveController _moveController;
    [HideInInspector]
    public InteractController _interactController;

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
        _swarm = GetComponent<Swarm>();
        _moveController = GetComponent<MoveController>();
        _interactController = GetComponent<InteractController>();
        _stateMachine = new StateMachine(this, FormType.Base);
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
