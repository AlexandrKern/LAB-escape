using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
    [HideInInspector]
    public HintController hintController;

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
        hintController.CleanTheHint();
    }

    public void EInteract()
    {
        _stateMachine.EInteract();
    }

    public void ChangeForm(FormType form)
    {
        if (form != FormType.Base)
            swarm.EnableFormObj((int)form); // включает объект формы, это нужно чтобы постоянно не держать их коллайдеры в поиске коллизии
                                            // и работы класса BottleneckDetector
        else
            swarm.DisableForms(); // если возвращаемся в рой, то выключаем объекты других форм
        
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
        hintController = GetComponent<HintController>();
    }

    private void Start()
    {
        UpdateStates();
    }

    public void UpdateStates()
    {
        _stateMachine.UpdateStatesCollection();
    }

    public FormType GetCharacterForm()
    {
        return _stateMachine.GetForm();
    }

    public void StateMachineUpdater()
    {
        _stateMachine.Update();
    }
}
