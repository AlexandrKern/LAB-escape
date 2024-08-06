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

    public void Interact()
    {
        _stateMachine.Interact();
    }

    public void ChangeForm(int index)
    {
        _stateMachine.EnterState(index);
        //TODO: transfer the forms change to states
        _swarm.SetFormIndex(index);
    }
    
    void Awake()
    {
        _swarm = GetComponent<Swarm>();
        _moveController = GetComponent<MoveController>();
        _interactController = GetComponent<InteractController>();
        _stateMachine = new StateMachine(this);
    }

    void Update()
    {
        _stateMachine.Update();
    }
}
